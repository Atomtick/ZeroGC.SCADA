using System;

namespace SCADA.HFSM
{
    internal static class EnumExtension
    {
        internal static string GetHashStringCode(this Enum @enum) => string.Format("{0}{1}{2}", ((IConvertible)@enum).ToInt32(null), @enum.GetType().AssemblyQualifiedName, @enum.ToString());

        internal static bool IsSame(this Enum @enum, Enum enum2) => @enum.GetType() == enum2.GetType() && @enum.ToInt32() == enum2.ToInt32() && @enum.ToString() == enum2.ToString();

        internal static int ToInt32(this Enum @enum) => ((IConvertible)@enum).ToInt32(null);
    }
}
