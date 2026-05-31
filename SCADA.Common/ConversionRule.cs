using System;

namespace SCADA.Common
{
    [Flags]
    public enum ConversionRule : byte
    {
        None = 0,
        CheckOverflow = 1,       // 溢出检查
        CheckPrecision = 2,      // 精度丢失检查
        AllowIntToFloat = 4,     // 允许 [整数] 转换为 [浮点数/Decimal]
        AllowFloatToInt = 8,     // 允许 [浮点数/Decimal] 转换为 [整数]
    }
}