using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using SCADA.Common;

namespace SCADA.Events
{
    public class EventManager
    {
        private object _lock = new object();

        private volatile EventInstance[] _alarmEventInstances = Array.Empty<EventInstance>();

        public void ClearAlarmEvent()
        {
            lock (_lock)
            {
                _alarmEventInstances = Array.Empty<EventInstance>();
            }
        }

        public void ClearAlarmEvent(string source) { }

        public void ClearAlarmEvent(long instanceId)
        {
            lock (_lock)
            {
                var alarms = _alarmEventInstances;
                GC.KeepAlive(alarms);
                if (alarms.Length == 0)
                {
                    return;
                }
                var newAlarms = alarms.Where(x => x.Id != instanceId).ToArray();
                _alarmEventInstances = newAlarms;
            }
        }

        public bool HasAlarmEvent(out IList<EventInstance> events)
        {
            var alarms = _alarmEventInstances;
            GC.KeepAlive(alarms);
            events = alarms;
            return alarms.Length > 0;
        }

        public bool HasAlarmEvent(string source, out IList<EventInstance> events)
        {
            var alarms = _alarmEventInstances;
            GC.KeepAlive(alarms);
            if (alarms.Length == 0)
            {
                events = null;
                return false;
            }
            var count = alarms.Count(x => x.Source == source);
            events = count > 0 ? alarms.Where(x => x.Source == source).ToArray() : null;
            return count > 0;
        }

        // 要判别事件是否重复，必须要有一个唯一的事件 Dvid，不能使用 EventDef 的 Id，因为 EventDef 是静态的，可能会被多个事件实例共享。

        private readonly ConcurrentDictionary<string, EventDef> _eventDefs = new ConcurrentDictionary<string, EventDef>();

        private Channel<EventInstance> _eventChannel;

        private long _eventIdCounter;

        public EventManager()
        {
            // 推荐使用 Bounded (有界队列) 防止消费者卡死时引发 OOM 内存爆炸
            var channelOptions = new BoundedChannelOptions(capacity: 5000)
            {
                // 如果队列满了，直接丢弃最老的数据 (或者选择 DropWrite 丢弃新数据，取决于你的业务语义)
                FullMode = BoundedChannelFullMode.DropOldest,

                // 🌟 极速优化的关键：告诉运行时，有且只有一个线程在消费！
                // 这会让 Channel 底层绕过复杂的并发锁，直接使用极简的无锁队列 (Lock-free Queue) 分支
                SingleReader = true,

                // 支持多个生产者 (比如多个 PLC 轮询线程同时往里塞数据)
                SingleWriter = false,

                // 🌟 危险配置警告：在半导体软件中强烈建议设为 false
                // 如果设为 true，生产者在写入时如果发现消费者在挂起等待，生产者会强行借用自己的线程去执行消费者的代码，
                // 这会导致你的底层硬件轮询线程被阻塞在消费逻辑上，直接导致机台通讯超时 (Timeout)！
                AllowSynchronousContinuations = false,
            };

            _eventChannel = Channel.CreateBounded<EventInstance>(channelOptions);

            Task.Run(async () =>
            {
                await foreach (var eventInstance in _eventChannel.Reader.ReadAllAsync())
                {
                    OnEventAsync?.Invoke(this, eventInstance);
                }
            });

            // 内置的三个事件类型，分别是信息、警告和报警，名称分别是 @、% 和 $，用于快速发布事件。
            Register(new EventDef(-1, "@", EventLevel.Info, "description", false));
            Register(new EventDef(-2, "%", EventLevel.Warn, "description", false));
            Register(new EventDef(-3, "$", EventLevel.Alarm, "description", false));
        }

        public event EventHandler<EventInstance> OnEventAsync;

        public event EventHandler<EventInstance> OnEventSync;

        #region 发布预定义事件

        public void PostEvent(string name, string source)
        {
            PostEvent(name, source, null, null);
        }

        public void PostEvent(string name, string source, ListDictionary DvidValues)
        {
            PostEvent(name, source, DvidValues, null);
        }

        #endregion 发布预定义事件

        #region 发布即时事件

        public void PostInfoEvent(string source, string description)
        {
            PostEvent("@", source, null, description);
        }

        public void PostInfoEvent(string source, string description, ListDictionary DvidValues)
        {
            PostEvent("@", source, DvidValues, description);
        }

        public void PostWarningEvent(string source, string description)
        {
            PostEvent("%", source, null, description);
        }

        public void PostWarningEvent(string source, string description, ListDictionary DvidValues)
        {
            PostEvent("%", source, DvidValues, description);
        }

        public void PostAlramEvent(string source, string description)
        {
            PostEvent("$", source, null, description);
        }

        public void PostAlramEvent(string source, string description, ListDictionary DvidValues)
        {
            PostEvent("$", source, DvidValues, description);
        }

        #endregion 发布即时事件

        #region 注册预定义事件

        public void Register(EventDef @event)
        {
            if (!_eventDefs.TryAdd(@event.Name, @event))
            {
                throw new InvalidOperationException($"Event with name '{@event.Name}' is already registered.");
            }
        }

        public void Register(long id, string name, EventLevel eventLevel, string description)
        {
            var eventDef = new EventDef((int)id, name, eventLevel, description, false);
            if (!_eventDefs.TryAdd(name, eventDef))
            {
                throw new InvalidOperationException($"Event with name '{eventDef.Name}' is already registered.");
            }
        }

        #endregion 注册预定义事件

        private void PostEvent(string name, string source, ListDictionary DvidValues, string description)
        {
            if (!_eventDefs.TryGetValue(name, out var eventDef))
            {
                throw new InvalidOperationException($"Event with name '{name}' is not registered.");
            }
            EventInstance eventInstance;
            if (eventDef.Name == "@" || eventDef.Name == "%" || eventDef.Name == "$")
            {
                eventInstance = new EventInstance
                {
                    Id = Interlocked.Increment(ref _eventIdCounter),
                    EventDef = eventDef.Clone(),
                    Source = source,
                    DvidValues = DvidValues,
                    OccurTime = DateTime.Now,
                };
                eventInstance.Description = description;
            }
            else
            {
                eventInstance = new EventInstance
                {
                    Id = Interlocked.Increment(ref _eventIdCounter),
                    EventDef = eventDef,
                    Source = source,
                    DvidValues = DvidValues,
                    OccurTime = DateTime.Now,
                };
                if (DvidValues != null && !string.IsNullOrEmpty(eventDef.DescriptionTemplate))
                {
                    eventInstance.Description = FormatDescription(eventDef.DescriptionTemplate, DvidValues);
                }
            }
            if (eventInstance.EventDef.Level == EventLevel.Alarm)
            {
                alarmEventInstances.Add(eventInstance);
            }
            _eventChannel.Writer.TryWrite(eventInstance);
            OnEventSync?.Invoke(this, eventInstance);
        }
    }
}
