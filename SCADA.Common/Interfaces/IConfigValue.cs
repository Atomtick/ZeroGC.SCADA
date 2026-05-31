using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Common.Interfaces
{
    public interface IConfigValue
    {
        bool IsAbsent();

        bool ToBool();

        bool ToBool(bool defaultValue);

        Color ToColor(Color defaultValue);

        Color ToColor();

        DateTime ToDateTime(DateTime defaultValue);

        DateTime ToDateTime();

        DirectoryInfo ToDirectory(DirectoryInfo defaultValue);

        DirectoryInfo ToDirectory();

        double ToDoble();

        double ToDouble(double defaultValue);

        FileInfo ToFile(FileInfo defaultValue);

        FileInfo ToFile();

        short ToInt16();

        short ToInt16(short defaultValue);

        int ToInt32();

        int ToInt32(int defaultValue);

        long ToInt64();

        long ToInt64(long defaultValue);

        sbyte ToInt8();

        sbyte ToInt8(sbyte defaultValue);

        float ToSingle(float defaultValue);

        float ToSingle();

        string ToString();

        string ToString(string defaultValue);

        ushort ToUInt16();

        ushort ToUInt16(ushort defaultValue);

        uint ToUInt32();

        uint ToUInt32(uint defaultValue);

        ulong ToUInt64();

        ulong ToUInt64(ulong defaultValue);

        byte ToUInt8();

        byte ToUInt8(byte defaultValue);

        bool TryToBool(out bool @bool);

        bool TryToColor(out Color color);

        bool TryToDateTime(out DateTime dateTime);

        bool TryToDirectory(out DirectoryInfo directoryInfo);

        bool TryToDouble(out double @double);

        bool TryToFile(out FileInfo fileInfo);

        bool TryToInt16(out short @short);

        bool TryToInt32(out int @int);

        bool TryToInt64(out long @long);

        bool TryToInt8(out sbyte @sbyte);

        bool TryToSingle(out float @float);

        bool TryToString(out string @string);

        bool TryToUInt16(out ushort @ushort);

        bool TryToUInt32(out uint @uint);

        bool TryToUInt64(out ulong @ulong);

        bool TryToUInt8(out byte @byte);
    }
}