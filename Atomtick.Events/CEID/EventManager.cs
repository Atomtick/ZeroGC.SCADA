using System;
using System.Collections;
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
using Atomtick.Common;
using SCADA.Common;

namespace Atomtick.Events.CEID
{
    // 采用快照模式,在接收到Alarm,Warning事件时,将事件实例存储在内存中,并提供查询接口,以便在需要时获取当前的Alarm事件列表。
    // Source -> Alarm
    // Module -> Alarm
    // Source -> Warning
    // Module -> Warning
    // 高频查询时,返回的是同一个列表,避免频繁创建新的列表对象,降低GC压力。

    public sealed class EventManager
    {
        // 要判别事件是否重复，必须要有一个唯一的事件 Dvid，不能使用 EventDef 的 Id，因为 EventDef 是静态的，可能会被多个事件实例共享。
        private readonly Channel<EventInstance> _eventChannel;

        private readonly ConcurrentDictionary<string, EventDef> _eventDefs = new ConcurrentDictionary<string, EventDef>();
        private readonly object _lock = new object();

        private volatile EventInstance[] _alertEventInstances = new EventInstance[0];
        private long _eventIdCounter;

        private EventManager()
        {
            // 内置的三个事件类型，分别是信息、警告和报警，名称分别是 @、% 和 $，用于快速发布事件。
            Register(new EventDef(-1, "?", EventLevel.Info, "", false));

            // 推荐使用 Bounded (有界队列) 防止消费者卡死时引发 OOM 内存爆炸
            var channelOptions = new BoundedChannelOptions(capacity: 1000)
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
                while (await _eventChannel.Reader.WaitToReadAsync())
                {
                    while (_eventChannel.Reader.TryRead(out var eventInstance))
                    {
                        OnEventAsync?.Invoke(this, eventInstance);
                    }
                }
            });
        }

        public event EventHandler<EventInstance> OnEventAsync;

        public event EventHandler<EventInstance> OnEventSync;

        public void ClearAlarmEvents()
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.EventDef.Level == EventLevel.Warn).ToArray();
                }
            }
        }

        public void ClearAlertEvent(long instanceId)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Id != instanceId).ToArray();
                }
            }
        }

        public void ClearAlertEvents()
        {
            lock (_lock)
            {
                _alertEventInstances = Array.Empty<EventInstance>();
            }
        }

        public void ClearModuleAlarmEvents(string module)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Module != module || x.EventDef.Level == EventLevel.Warn).ToArray();
                }
            }
        }

        public void ClearModuleAlertEvents(string module)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Module != module).ToArray();
                }
            }
        }

        public void ClearModuleWarnEvents(string module)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Module != module || x.EventDef.Level == EventLevel.Alarm).ToArray();
                }
            }
        }

        public void ClearSourceAlarmEvents(string source)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Source != source || x.EventDef.Level == EventLevel.Warn).ToArray();
                }
            }
        }

        public void ClearSourceAlertEvents(string source)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Source != source).ToArray();
                }
            }
        }

        public void ClearSourceWarnEvents(string source)
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.Source != source || x.EventDef.Level == EventLevel.Alarm).ToArray();
                }
            }
        }

        public void ClearWarnEvents()
        {
            lock (_lock)
            {
                var alertEventInstances = _alertEventInstances;
                if (alertEventInstances.Length > 0)
                {
                    _alertEventInstances = alertEventInstances.Where(x => x.EventDef.Level == EventLevel.Alarm).ToArray();
                }
            }
        }

        public void GetAlertEvents(out IList<EventInstance> alarms, out IList<EventInstance> warns)
        {
            alarms = null;
            warns = null;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (alertEventInstance.EventDef.Level == EventLevel.Alarm)
                    {
                        if (alarms == null)
                        {
                            alarms = new List<EventInstance>();
                        }
                        alarms.Add(alertEventInstance);
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Warn)
                    {
                        if (warns == null)
                        {
                            warns = new List<EventInstance>();
                        }
                        warns.Add(alertEventInstance);
                    }
                }
            }
        }

        public void GetModuleAlertEvents(string module, out IList<EventInstance> alarms, out IList<EventInstance> warns)
        {
            alarms = null;
            warns = null;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (alertEventInstance.Module == module)
                    {
                        if (alertEventInstance.EventDef.Level == EventLevel.Alarm)
                        {
                            if (alarms == null)
                            {
                                alarms = new List<EventInstance>();
                            }
                            alarms.Add(alertEventInstance);
                        }
                        if (alertEventInstance.EventDef.Level == EventLevel.Warn)
                        {
                            if (warns == null)
                            {
                                warns = new List<EventInstance>();
                            }
                            warns.Add(alertEventInstance);
                        }
                    }
                }
            }
        }

        public void GetSourceAlertEvents(string source, out IList<EventInstance> alarms, out IList<EventInstance> warns)
        {
            alarms = null;
            warns = null;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (alertEventInstance.Source == source)
                    {
                        if (alertEventInstance.EventDef.Level == EventLevel.Alarm)
                        {
                            if (alarms == null)
                            {
                                alarms = new List<EventInstance>();
                            }
                            alarms.Add(alertEventInstance);
                        }
                        if (alertEventInstance.EventDef.Level == EventLevel.Warn)
                        {
                            if (warns == null)
                            {
                                warns = new List<EventInstance>();
                            }
                            warns.Add(alertEventInstance);
                        }
                    }
                }
            }
        }

        public void HasAlertEvent(out bool hasAlarm, out bool hasWarn)
        {
            hasAlarm = false;
            hasWarn = false;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (hasAlarm && hasWarn)
                    {
                        break;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Alarm)
                    {
                        hasAlarm = true;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Warn)
                    {
                        hasWarn = true;
                    }
                }
            }
        }

        public void HasModuleAlertEvent(string module, out bool hasAlarm, out bool hasWarn)
        {
            hasAlarm = false;
            hasWarn = false;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (hasAlarm && hasWarn)
                    {
                        break;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Alarm && alertEventInstance.Module == module)
                    {
                        hasAlarm = true;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Warn && alertEventInstance.Module == module)
                    {
                        hasWarn = true;
                    }
                }
            }
        }

        public void HasSourceAlertEvent(string source, out bool hasAlarm, out bool hasWarn)
        {
            hasAlarm = false;
            hasWarn = false;
            var alertEventInstances = _alertEventInstances;
            if (alertEventInstances.Length > 0)
            {
                foreach (var alertEventInstance in alertEventInstances)
                {
                    if (hasAlarm && hasWarn)
                    {
                        break;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Alarm && alertEventInstance.Source == source)
                    {
                        hasAlarm = true;
                    }
                    if (alertEventInstance.EventDef.Level == EventLevel.Warn && alertEventInstance.Source == source)
                    {
                        hasWarn = true;
                    }
                }
            }
        }

        #region 发布预定义事件

        public void PostEvent(string name, string source)
        {
            PostEvent(name, source, null, null);
        }

        public void PostEvent(string name, string source, ListDict DvidValues)
        {
            PostEvent(name, source, DvidValues, null);
        }

        #endregion 发布预定义事件

        #region 发布未定义事件

        public void PostUndefEvent(string source, EventLevel eventLevel, string description)
        {
            PostUndefEvent(source, eventLevel, description, null);
        }

        public void PostUndefEvent(string source, EventLevel eventLevel, string description, ListDict DvidValues)
        {
            PostEvent("?", source, DvidValues, description, eventLevel);
        }

        #endregion 发布未定义事件

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

        private string FormatDescription(string template, ListDict DvidValues)
        {
            if (DvidValues == null || DvidValues.Count == 0)
            {
                return template;
            }
            StringBuilder sb = new StringBuilder(template);
            foreach (var entry in DvidValues)
            {
                string placeholder = $"{{{entry.Key}}}";
                string value = entry.Value != null ? $"'{entry.Value}'" : "' '";
                sb.Replace(placeholder, value);
            }
            return sb.ToString();
        }

        private void PostEvent(string name, string source, ListDict DvidValues, string description, EventLevel eventLevel = EventLevel.Info)
        {
            if (!_eventDefs.TryGetValue(name, out var eventDef))
            {
                throw new InvalidOperationException($"Event with name '{name}' is not registered.");
            }
            EventInstance eventInstance = new EventInstance
            {
                Id = Interlocked.Increment(ref _eventIdCounter),
                EventDef = eventDef,
                Source = source,
                DvidValues = DvidValues,
                OccurTime = DateTime.Now,
            };
            if (eventDef.Name == "?")
            {
                eventDef.Level = eventLevel;
                eventInstance.Description = description;
            }
            else
            {
                if (DvidValues != null && !string.IsNullOrEmpty(eventDef.DescriptionTemplate))
                {
                    eventInstance.Description = FormatDescription(eventDef.DescriptionTemplate, DvidValues);
                }
            }
            if (eventInstance.EventDef.Level == EventLevel.Alarm || eventInstance.EventDef.Level == EventLevel.Warn)
            {
                lock (_lock)
                {
                    var alerts = _alertEventInstances;
                    var newAlerts = new EventInstance[alerts.Length + 1];
                    Array.Copy(alerts, newAlerts, alerts.Length);
                    newAlerts[alerts.Length] = eventInstance;
                    _alertEventInstances = newAlerts;
                }
            }
            OnEventSync?.Invoke(this, eventInstance);
            _eventChannel.Writer.TryWrite(eventInstance);
        }
    }
}
