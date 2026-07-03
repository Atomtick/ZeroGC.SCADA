using System;
using System.Collections.Generic;

namespace SCADA.SecsGem
{
    public enum SecsDataType
    {
        ASCII,
        U1,
        U2,
        U4,
        I2,
        I4,
        F4,
        F8,
        Boolean,
        List
    }
    
    public abstract class SecsVariableBase
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public string Units { get; set; }
        public SecsDataType  DataType { get; set; }
    }
    
    /// <summary>
    /// 状态变量 (Status Variable)
    /// </summary>
    public class Svid : SecsVariableBase
    {
        // Host 无权修改 SVID，仅限设备内部或底层驱动写入
    
        /// <summary>
        /// 记录最后一次设备更新此状态值的时间戳 (可选，但非常实用)
        /// </summary>
        public DateTime LastUpdated { get; internal set; }

        /// <summary>
        /// 标识此变量是否正在被 Host 采样 (Trace, S2F23)
        /// </summary>
        public bool IsTraced { get; set; }
    }
    
    /// <summary>
    /// 设备常量 (Equipment Constant)
    /// </summary>
    public class Ecid : SecsVariableBase
    {
        /// <summary>
        /// 默认值 (Default Value)
        /// 当设备重置或初始化时恢复的值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 最小值下限 (Minimum Value)
        /// 用于数值类型 (U, I, F) 的写入校验
        /// </summary>
        public object MinValue { get; set; }

        /// <summary>
        /// 最大值上限 (Maximum Value)
        /// 用于数值类型 (U, I, F) 的写入校验
        /// </summary>
        public object MaxValue { get; set; }

        /// <summary>
        /// 枚举型常量的合法值列表 (Allowed Values)
        /// 用于 ASCII 或离散数值 (例如: 允许值为 "ON", "OFF", "AUTO")
        /// </summary>
        public object[] AllowedValues { get; set; }

        /// <summary>
        /// 标识在设备处于 Running (Processing) 状态时，是否允许 Host 修改此常量
        /// </summary>
        public bool IsEditableDuringProcess { get; set; }
    }
    
    /// <summary>
    /// 数据变量 (Data Value)
    /// </summary>
    public class Dvid : SecsVariableBase
    {
        /// <summary>
        /// 作用域/绑定的事件 ID 列表 (Associated CEIDs)
        /// 记录这个 DVID 在哪些事件发生时才包含有效数据。
        /// 这对于防止 Host 读到“脏数据”至关重要。
        /// </summary>
        public List<uint> ValidForEvents { get; set; } = new List<uint>();

        // 提示: DVID 的 Value 属性通常只有在对应的 CEID 触发前那一刻，
        // 才会被底层设备驱动赋值。事件发送完毕后，其值可能会被置空或失效。
    }
}
