using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atomtick.Configuration.Interfaces
{
    public interface IConfigConverter
    {
        void CheckConfigFormattingValid(string config);
        string[] CheckConfigItemFormatting(string config);
        object Convert2Object(ConfigType type, string value);
        string Convert2String(object value);
        bool TryParse2Color(string @string, out Color color);
        bool TryParse2DateTime(string @string, out DateTime dateTime);
        bool TryParse2Directory(string @string, out DirectoryInfo directoryInfo);
        bool TryParse2Double(string @string, out double @double);
        bool TryParse2File(string @string, out FileInfo fileInfo);
        bool TryParse2Int64(string @string, out long @long);
    }
}
