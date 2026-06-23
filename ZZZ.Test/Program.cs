using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using SCADA.Common.Triggers;
using SCADA.Configuration;

namespace ZZZ.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configSource = new PrimitiveConfigSource("configs.db");

            configSource.BeginTransaction(out long transactionId);

            configSource
                .Write(transactionId, "Cylinder.Timeout", 5 * 1000)
                .Write(transactionId, "EAP.IP", "192.168.1.29")
                .Write(transactionId, "Log.Enable", true)
                .Write(transactionId, "FlowRate.Tolerance", 1.5)
                .Write(transactionId, "Alarm.Color", System.Drawing.Color.Red)
                .Write(transactionId, "Data.Folder", "C:\\Logs");

            configSource.CommitTransaction(transactionId);
        }
    }

    namespace IndustrialControl.Components
    {
        // 1. 定义极其明确的状态字典
        public enum CylinderState
        {
            Unknown, // 刚开机，不知道在哪
            Retracted, // 已退回 (原点)
            Extended, // 已伸出 (工作点)
            MovingToRetract, // 正在退回
            MovingToExtend, // 正在伸出
            Error, // 故障/超时状态
        }

        // 2. 定义外部可以下发的合法指令
        public enum CylinderCommand
        {
            None = 0,
            Retract = 1,
            Extend = 2,
            ResetError = 3,
        }

        public class StandardCylinder
        {
            public string Name { get; }
            public CylinderState CurrentState { get; private set; } = CylinderState.Unknown;

            // 使用 Interlocked 实现无锁的指令信箱，极大地提升高并发下的性能
            private int _pendingCommand = (int)CylinderCommand.None;

            // 超时设置与高精度计时器 (这里可以用我们之前讨论的 TonTimer struct)
            private readonly TimeSpan _timeout;

            private long _moveStartTime;

            // 硬件 IO 映射接口 (通常通过接口注入，这里为了直观用委托或抽象方法代替)
            // 假设是双作用气缸，带两个传感器
            private bool _valveExtendOutput;

            private bool _valveRetractOutput;

            public StandardCylinder(string name, TimeSpan timeout)
            {
                Name = name;
                _timeout = timeout;
            }

            /// <summary>
            /// 【外部接口】供 UI、网络或上层配方序列调用。
            /// 绝对线程安全，立即返回，不阻塞。
            /// </summary>
            public void SendCommand(CylinderCommand cmd)
            {
                // 将新指令原子性地放入信箱，覆盖旧指令
                Interlocked.Exchange(ref _pendingCommand, (int)cmd);
            }

            /// <summary>
            /// 【核心主循环】由控制引擎的 Scan 线程周期性调用 (例如每 10ms)。
            /// 所有的物理逻辑都在这个单线程上下文中执行，彻底消灭并发 Bug。
            /// </summary>
            /// <param name="sensorRetracted">当前退回位传感器状态 (通常从 PLC 批量读取后传入)</param>
            /// <param name="sensorExtended">当前伸出位传感器状态</param>
            public void Monitor(bool sensorRetracted, bool sensorExtended)
            {
                // 1. 取出并清空信箱中的指令 (原子操作)
                var cmd = (CylinderCommand)Interlocked.Exchange(ref _pendingCommand, (int)CylinderCommand.None);

                // 2. 处理外部指令 (改变状态机的走向)
                ProcessCommand(cmd, sensorRetracted, sensorExtended);

                // 3. 执行状态机当前状态对应的逻辑 (包含超时监控和到位检测)
                EvaluateState(sensorRetracted, sensorExtended);
            }

            private void ProcessCommand(CylinderCommand cmd, bool sensorRetracted, bool sensorExtended)
            {
                if (cmd == CylinderCommand.None)
                    return;

                if (cmd == CylinderCommand.ResetError)
                {
                    CurrentState = CylinderState.Unknown; // 复位后重新评估位置
                    return;
                }

                // 如果处于错误状态，拒绝执行任何移动指令，必须先复位
                if (CurrentState == CylinderState.Error)
                    return;

                if (cmd == CylinderCommand.Extend && CurrentState != CylinderState.Extended)
                {
                    SetOutputs(extend: true, retract: false);
                    CurrentState = CylinderState.MovingToExtend;
                    _moveStartTime = Stopwatch.GetTimestamp();
                }
                else if (cmd == CylinderCommand.Retract && CurrentState != CylinderState.Retracted)
                {
                    SetOutputs(extend: false, retract: true);
                    CurrentState = CylinderState.MovingToRetract;
                    _moveStartTime = Stopwatch.GetTimestamp();
                }
            }

            private void EvaluateState(bool sensorRetracted, bool sensorExtended)
            {
                switch (CurrentState)
                {
                    case CylinderState.Unknown:
                        // 开机自检或复位后的状态推断
                        if (sensorRetracted && !sensorExtended)
                            CurrentState = CylinderState.Retracted;
                        else if (!sensorRetracted && sensorExtended)
                            CurrentState = CylinderState.Extended;
                        break;

                    case CylinderState.MovingToExtend:
                        if (sensorExtended)
                        {
                            CurrentState = CylinderState.Extended; // 到位，状态流转
                        }
                        else if (Stopwatch.GetElapsedTime(_moveStartTime) > _timeout)
                        {
                            TriggerAlarm("Extend Timeout! Cylinder may be jammed.");
                        }
                        break;

                    case CylinderState.MovingToRetract:
                        if (sensorRetracted)
                        {
                            CurrentState = CylinderState.Retracted; // 到位，状态流转
                        }
                        else if (Stopwatch.GetElapsedTime(_moveStartTime) > _timeout)
                        {
                            TriggerAlarm("Retract Timeout! Cylinder may be jammed.");
                        }
                        break;

                    case CylinderState.Extended:
                        // 静态防呆：如果处于伸出状态，但传感器突然丢失，可能发生掉气或机械故障
                        if (!sensorExtended)
                        {
                            TriggerAlarm("Lost Extend Sensor while in Extended state!");
                        }
                        break;

                    case CylinderState.Retracted:
                        if (!sensorRetracted)
                        {
                            TriggerAlarm("Lost Retract Sensor while in Retracted state!");
                        }
                        break;
                }
            }

            private void SetOutputs(bool extend, bool retract)
            {
                _valveExtendOutput = extend;
                _valveRetractOutput = retract;
                // 实际工程中，这里会将状态写入到一个发送给 PLC 的映射内存块中
                // 例如：PlcMemoryMap.WriteValve(Name, _valveExtendOutput);
            }

            private void TriggerAlarm(string message)
            {
                CurrentState = CylinderState.Error;
                SetOutputs(false, false); // 发生错误时通常需要切断输出，具体视工艺安全要求而定
                Console.WriteLine($"[ALARM - {Name}] {message}");
            }
        }
    }
}
