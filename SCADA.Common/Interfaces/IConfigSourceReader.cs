using System;
using System.Collections.Generic;

namespace SCADA.Common.Interfaces
{
    public interface IConfigSourceReader
    {
        IConfigItem SelectConfigItem(string config);
        void Read(IConfigItem configItem, out IConfigValue value);
        void Read(IConfigItem configItem1, out IConfigValue value1, IConfigItem configItem2, out IConfigValue value2);
        void Read(IConfigItem configItem1, out IConfigValue value1, IConfigItem configItem2, out IConfigValue value2, IConfigItem configItem3, out IConfigValue value3);
    }
}