using System;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace SCADA.ObjectModel
{
    // 1. 数据采集功能 2. AlarmEvent注册抛出功能 3.脚本功能
    public abstract class DeviceBase : IDevice, IModular
    {
        protected DeviceBase(string paramsString, ConstructorParamsFormat format)
        {
            if (format == ConstructorParamsFormat.XML)
            {
                XElement xml = XElement.Parse(paramsString);
                xml.Attribute("name").Value = paramsString;
            }
            else if (format == ConstructorParamsFormat.JSON)
            {
                JsonNode json = JsonNode.Parse(paramsString);
            }
            else if (format == ConstructorParamsFormat.INI)
            {
                throw new NotImplementedException();
            }
            else if (format == ConstructorParamsFormat.TOML)
            {
                throw new NotImplementedException();
            }
            else if (format == ConstructorParamsFormat.YAML)
            {
                throw new NotImplementedException();
            }
        }

        public string Id => Module + "." + Name;
        public string Module { get; }
        public string Name { get; }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Monitor()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}