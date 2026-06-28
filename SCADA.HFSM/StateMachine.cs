using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using SCADA.HFSM.Enums;
using SCADA.HFSM.Interfaces;

namespace SCADA.HFSM
{
    //  用ENUM之间的关系表示子状态机和父状态机
    // 0X01 - 0XFF
    // 0X0101
    internal struct Msg<TCommand>
        where TCommand : Enum
    {
        public TCommand Command { get; set; }
        public object[] Args { get; set; }
    }

    public class StateMachine<TState, TCommand> : IStateMachine
        where TState : Enum
        where TCommand : Enum
    {
        private readonly int _defaultInterval;
        private readonly ExceptionThrownEventArgs _exceptionThrownEventArgs;
        private readonly Lock _lockObj;
        private readonly ConcurrentQueue<Msg<TCommand>> _msgQueue;

        //private readonly Channel<(Enum msgCmmd, object[] args)> _msgQueue;
        private readonly Lock _postMsgLock;
        private readonly StateTransitedEventArgs _stateTransitedEventArgs;
        private readonly StateTransitingEventArgs _stateTransitingEventArgs;
        private readonly Dictionary<string, List<(TCommand msgCmd, Func<object[], bool> action, TState nextState)>> _transitionTable;
        private readonly List<(TCommand msgCmd, Func<object[], bool> action, TState nextState)>[] _transitionTable2 = new List<(TCommand msgCmd, Func<object[], bool> action, TState nextState)>[256];
        private Enum _currState;
        private TaskCompletionSource<bool> _pauseSource;
        private Enum _prevState;
        private Task<(Enum cmd, object[] args)> _readTask;
        private int _tempInterval;

        #region Constructors

        public StateMachine(string name)
            : this(name, FsmState.None, 75) { }

        private StateMachine(string name, Enum initialState, int interval)
        {
            _transitionTable = new Dictionary<string, List<(TCommand msg, Func<object[], bool> action, TState nextState)>>();
            _msgQueue = new ConcurrentQueue<Msg<TCommand>>();
            _pauseSource = null;
            Name = name;
            _prevState = FsmState.None;
            _currState = initialState;
            _tempInterval = -1;
            _defaultInterval = interval;
            _postMsgLock = new Lock();
            _lockObj = new Lock();
            _stateTransitedEventArgs = new StateTransitedEventArgs(FsmState.None, FsmState.None, FsmMsgCmd.None, null);
            _stateTransitingEventArgs = new StateTransitingEventArgs(FsmState.None, FsmState.None, FsmMsgCmd.None, null);
            _exceptionThrownEventArgs = new ExceptionThrownEventArgs(null, FsmState.None, FsmState.None, FsmMsgCmd.None, null);
            Task.Run(Loop);
        }

        #endregion Constructors

        public event EventHandler<ExceptionThrownEventArgs> ActionThrownException;

        public event EventHandler<StateTransitingEventArgs> MsgNotMatchAnyState;

        public event EventHandler<StateTransitedEventArgs> StateEntered;

        public event EventHandler<StateTransitingEventArgs> StateExited;

        public Enum CurrState => _currState;
        public string Name { get; }
        public Enum PrevState => _prevState;

        #region Interval

        private int Interval
        {
            get
            {
                if (_tempInterval <= -1)
                {
                    return _defaultInterval;
                }
                return _tempInterval;
            }
        }

        public void AdjustInterval(int interval)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(interval, -1);
            _tempInterval = interval;
        }

        public void RestoreDefaultInterval()
        {
            _tempInterval = -1;
        }

        #endregion Interval

        #region CanMatch

        bool IFsmTransitionTable.CanMatch(TCommand msgCmd, TState state)
        {
            var anyHashCode = FsmState.Any.GetHashStringCode();
            if (_transitionTable.ContainsKey(anyHashCode) && _transitionTable[anyHashCode].Any(item => item.msgCmd.IsSame(msgCmd)))
                return true;
            var stateHashCode = state.GetHashStringCode();
            if (_transitionTable.ContainsKey(stateHashCode) && _transitionTable[stateHashCode].Any(item => item.msgCmd.IsSame(msgCmd)))
                return true;
            return false;
        }

        bool IFsmTransitionTable.CanMatch(TCommand msgCmd, TState state, out Func<object[], bool> action, out TState nextState)
        {
            action = default;
            nextState = default;

            var stateHashCode = state.GetHashStringCode();
            if (_transitionTable.TryGetValue(stateHashCode, out List<(Enum msgCmd, Func<object[], bool> action, TState nextState)> value))
            {
                var index = value.FindIndex(item => item.msgCmd.IsSame(msgCmd));
                if (index > -1)
                {
                    action = _transitionTable[stateHashCode][index].action;
                    nextState = _transitionTable[stateHashCode][index].nextState;
                    return true;
                }
            }
            return false;
        }

        #endregion CanMatch

        public void ClearMsgQueue()
        {
            while (_msgQueue.TryDequeue(out var _))
                ;
        }

        #region Pause & Resume

        public void Stop()
        {
            lock (_lockObj)
            {
                _pauseSource ??= new TaskCompletionSource<bool>(false, TaskCreationOptions.RunContinuationsAsynchronously);
            }
        }

        public void Start()
        {
            TaskCompletionSource<bool> tcsToSignal = null;
            lock (_lockObj)
            {
                if (_pauseSource != null)
                {
                    // 取出当前的 TCS 引用
                    tcsToSignal = _pauseSource;
                    // 立即将字段置空，标志着“不再暂停”
                    _pauseSource = null;
                }
                // 在锁外部触发 SetResult，减少锁的持有时间
                // TrySetResult 是线程安全的
                tcsToSignal?.TrySetResult(true);
            }
        }

        #endregion Pause & Resume


        public (bool isSuccess, TState currentState) PostMsg(TCommand msgCmd, params object[] args)
        {
            var currentState = Volatile.Read(ref _currState);
            lock (_postMsgLock)
            {
                if (((IStateMachine)this).CanMatch(msgCmd, currentState) == false)
                {
                    return (false, currentState);
                }
                _msgQueue.Writer.TryWrite((msgCmd, args));
                return (true, currentState);
            }
        }

        #region Register
        public ref struct RefArgsReader { }

        public void Register(TState currState, TCommand msgCmd, TState nextState, Func<object[], bool> action)
        {
            var stateHashCode = currState.GetHashStringCode();
            if (!_transitionTable.TryGetValue(stateHashCode, out List<(Enum msgCmd, Func<object[], bool> action, Enum nextState)> value))
            {
                value = new List<(Enum msg, Func<object[], bool> action, Enum nextState)>();
                _transitionTable[stateHashCode] = value;
            }
            // 一个状态被不同的消息驱动到不同的状态，但是同一个状态不应该被同一个消息驱动到不同的状态
            if (value.Any(x => x.msgCmd.IsSame(msgCmd)))
            {
                throw new Exception($"State Machine Transition Table Conflict: State {currState} has already registered message {msgCmd}.");
            }
            value.Add((msgCmd, action, nextState));
        }

        // 不需要执行Action，接收到消息直接转换状态
        public void Register(TState currState, TCommand msgCmd, TState nextState)
        {
            Register(currState, nextState, msgCmd, static (args) => true);
        }

        public void Register(TState currState, TCommand msgCmd, RoutineBase routine, TState relayState, TState nextState, TState abortState)
        {
            Register(currState, relayState, msgCmd, routine.Start);
            Register(relayState, FsmState.None, FsmMsgCmd.Timer, routine.Steps);
            Register(relayState, nextState, FsmMsgCmd.Complete, static (args) => true);
            Register(relayState, FsmState.Error, FsmMsgCmd.Error, routine.Error);
            Register(relayState, abortState, FsmMsgCmd.Abort, routine.Abort);
        }

        public void SetMonitor(Enum state, Action monitor)
        {
            Register(
                state,
                FsmState.None,
                FsmMsgCmd.Timer,
                (args) =>
                {
                    try
                    {
                        monitor.Invoke();
                    }
                    catch { }
                    return false;
                }
            );
        }

        #endregion Register

        private void Monitor()
        {
            /*******************暂停机制************************/
            var pauseSource = Volatile.Read(ref _pauseSource);
            if (pauseSource != null)
            {
                await pauseSource.Task.ConfigureAwait(false);
            }
            /***************************************************/

            /*******************读取消息.若期望时间内无新消息,则不再等待,直接返回一条虚拟消息FsmMsgCmd.Timer***********************/
            (Enum cmd, object[] args) msg;
            _readTask ??= _msgQueue.Reader.ReadAsync().AsTask();
            var delayTask = Task.Delay(Interval); //Interval是0时，返回一个CompletedTask，不会让出CPU和线程，是直通绿道。它与Task.Yield()不同。
            var completedTask = await Task.WhenAny(delayTask, _readTask).ConfigureAwait(false);
            if (completedTask == _readTask)
            {
                msg = await _readTask.ConfigureAwait(false);
                _readTask = null;
            }
            else
            {
                msg = (FsmMsgCmd.Timer, null);
            }
            /***********************************************************************************************************/

            /*******************暂停机制************************/
            pauseSource = Volatile.Read(ref _pauseSource);
            if (pauseSource != null)
            {
                await pauseSource.Task.ConfigureAwait(false); // 暂停机制
            }
            /***************************************************/

            var match = ((IStateMachine)this).CanMatch(msg.cmd, _currState, out Func<object[], bool> action, out Enum nextState);

            // 当前状态不接受此消息
            if (match == false)
            {
                // 比如Error入队了，它的Action一下就立刻完成了，如果Action不小心返回false，可能还会错误的执行Step，如果返回true，成功切入到Error状态，
                // 这时候一直Timer消息，但是Timer并没有注册，这不算错误。
                if (msg.cmd.IsSame(FsmMsgCmd.Timer) == false)
                {
                    _stateTransitingEventArgs.CurrState = _currState;
                    _stateTransitingEventArgs.NextState = nextState;
                    _stateTransitingEventArgs.MsgCmd = msg.cmd;
                    _stateTransitingEventArgs.MsgArgs = msg.args;

                    try
                    {
                        MsgNotMatchAnyState?.Invoke(this, _stateTransitingEventArgs);
                    }
                    catch { }
                }
                continue;
            }

            // 如果action异常,就继续保持原状态
            bool actionResult = false;
            try
            {
                actionResult = action.Invoke(msg.args);
            }
            catch (Exception ex)
            {
                _exceptionThrownEventArgs.Exception = ex;
                _exceptionThrownEventArgs.CurrState = _currState;
                _exceptionThrownEventArgs.NextState = nextState;
                _exceptionThrownEventArgs.MsgCmd = msg.cmd;
                _exceptionThrownEventArgs.MsgArgs = msg.args;
                ActionThrownException?.Invoke(this, _exceptionThrownEventArgs);
            }

            // action返回true,切换到下一状态;返回false,保持原状态.
            if (actionResult)
            {
                _stateTransitingEventArgs.CurrState = _currState;
                _stateTransitingEventArgs.NextState = nextState;
                _stateTransitingEventArgs.MsgCmd = msg.cmd;
                _stateTransitingEventArgs.MsgArgs = msg.args;
                try
                {
                    StateExited?.Invoke(this, _stateTransitingEventArgs);
                }
                catch { }

                _prevState = _currState;
                _currState = nextState;

                _stateTransitedEventArgs.CurrState = _currState;
                _stateTransitedEventArgs.PreviousState = _prevState;
                _stateTransitedEventArgs.MsgCmd = msg.cmd;
                _stateTransitedEventArgs.MsgArgs = msg.args;
                try
                {
                    StateEntered?.Invoke(this, _stateTransitedEventArgs);
                }
                catch { }
            }
        }

        private async void Loop()
        {
            while (true)
            {
                Monitor();
            }
        }
    }
}
