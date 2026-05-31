using SCADA.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SCADA.Configuration
{
    public enum ConfigType
    {
        Bool,
        Integer,
        Decimal,
        String,
        File,
        Folder,
        DateTime,
        Color
    }
}