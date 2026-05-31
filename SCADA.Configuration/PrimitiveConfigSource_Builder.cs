using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource
    {
        public ConfigNode[] RootNodes { get; private set; }
        private readonly List<ConfigNode> _rootNodes = new List<ConfigNode>();

        public void LoadSqlite(string dbFilePath)
        {

        }

        public void LoadXml(string xml)
        {
            // 建议配置 XmlReaderSettings，例如忽略无关的空白节点，进一步提升性能
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true,
            };

            using (StringReader stream = new StringReader(xml))
            {
                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    reader.Read(); // XML声明
                    reader.Read(); // root根节点
                    Build(reader);
                }
            }
        }

        private void Build(XmlReader xmlReader)
        {

            string GetHint()
            {
                return $"LineNumber:{(xmlReader as IXmlLineInfo)?.LineNumber}, LinePosition:{(xmlReader as IXmlLineInfo)?.LinePosition}";
            }

            var nodeAllowedAttributes = new string[] { "name", "display", "visible", "enable" };
            var itemAllowedAttributes = new string[]
            {
                "initial_value", "name", "display", "type","desc",
                "min","max","options", "regex", "regex_note",
                "unit", "visible", "enable",  "restart",
            };
            var nodeStack = new Stack<ConfigNode>(8);
            while (xmlReader.Read())
            {
                string name;
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    // name
                    name = xmlReader.GetAttribute("name");
                    if (name == null)
                        throw new ArgumentException(
                            $"The 'name' attribute is required. Hint:'{GetHint()}'");
                    name = name.Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new ArgumentException(
                            $"The 'name' attribute cannot be empty. Hint:'{GetHint()}'");
                    }

                    if (name.Contains("."))
                    {
                        throw new ArgumentException(
                            $"The 'name' attribute can't contain '.'. Hint:'{GetHint()}'");
                    }

                    // display
                    var display = xmlReader.GetAttribute("display");
                    display = display == null || string.IsNullOrWhiteSpace(display) ? name : display.Trim();
                    // visible
                    var visible = xmlReader.GetAttribute("visible");
                    if (visible == null || string.IsNullOrWhiteSpace(visible))
                    {
                        visible = true.ToString();
                    }
                    else
                    {
                        visible = visible.Trim();
                        if (!bool.TryParse(visible, out _))
                        {
                            throw new ArgumentException(
                                $"The 'visible' attribute must be a boolean value. Hint:'{GetHint()}'");
                        }
                    }
                    // enable
                    var enable = xmlReader.GetAttribute("enable");
                    if (enable == null || string.IsNullOrWhiteSpace(enable))
                    {
                        enable = true.ToString();
                    }
                    else
                    {
                        enable = enable.Trim();
                        if (!bool.TryParse(enable, out _))
                        {
                            throw new ArgumentException(
                                $"The 'enable' attribute must be a boolean value. Hint:'{GetHint()}'");
                        }
                    }

                    // 配置类别节点
                    if (xmlReader.GetAttribute("type") == null
                        && xmlReader.GetAttribute("initial_value") == null)
                    {
                        if (xmlReader.AttributeCount > 0)
                        {
                            while (xmlReader.MoveToNextAttribute())
                            {
                                if (!nodeAllowedAttributes.Any(x => x.Equals(xmlReader.Name)))
                                {
                                    throw new ArgumentException(
                                        $"Unsupported  attribute '{xmlReader.Name}'. Hint:'{GetHint()}'");
                                }
                            }
                            // 永远不要忘记在遍历完属性后调用 reader.MoveToElement()，让读取器的指针从“属性上下文”退回到“元素上下文”
                            xmlReader.MoveToElement();
                        }

                        var node = new ConfigNode
                        {
                            Name = name,
                            Display = display,
                            Enable = bool.Parse(enable),
                            Visible = bool.Parse(visible)
                        };
                        if (nodeStack.Count > 0)
                        {
                            if (nodeStack.Peek().Children.Any(c => c.Name == name))
                            {
                                throw new ArgumentException(
                                    $"The 'name' attribute must be unique within the same node's children. Hint:'{GetHint()}'");
                            }
                            nodeStack.Peek().Children.Add(node);
                            node.Parent = nodeStack.Peek();
                            nodeStack.Push(node);
                        }
                        else
                        {
                            if (_rootNodes.Any(c => c.Name == name))
                            {
                                throw new ArgumentException(
                                    $"The 'name' attribute must be unique for top-level nodes. Hint:'{GetHint()}'");
                            }
                            _rootNodes.Add(node);
                            nodeStack.Push(node);
                        }
                    }

                    // 配置项节点
                    else if (xmlReader.GetAttribute("type") != null
                             && xmlReader.GetAttribute("initial_value") != null)
                    {
                        if (xmlReader.AttributeCount > 0)
                        {
                            while (xmlReader.MoveToNextAttribute())
                            {
                                if (!itemAllowedAttributes.Any(x => x.Equals(xmlReader.Name)))
                                {
                                    throw new ArgumentException(
                                        $"Unsupported  attribute '{xmlReader.Name}'. Hint:'{GetHint()}'");
                                }
                            }
                            // 永远不要忘记在遍历完属性后调用 reader.MoveToElement()，让读取器的指针从“属性上下文”退回到“元素上下文”
                            xmlReader.MoveToElement();
                        }

                        string description, unit, regex, regexNote, restart;

                        // type
                        var type = xmlReader.GetAttribute("type");
                        if (string.IsNullOrWhiteSpace(type))
                        {
                            throw new ArgumentException(
                                $"The 'type' attribute cannot be empty. Hint:'{GetHint()}'");
                        }

                        type = type.Trim();
                        if (!Enum.TryParse<ConfigType>(type, true, out var configType))
                        {
                            throw new ArgumentException(
                                $"Invalid value for 'type' attribute: '{type}'. Hint:'{GetHint()}'");
                        }

                        // description
                        description = xmlReader.GetAttribute("desc");
                        description = string.IsNullOrWhiteSpace(description) ? string.Empty : description.Trim();

                        // min
                        // decimal 和 integer 才允许配置 min
                        var min = xmlReader.GetAttribute("min");
                        if (configType != ConfigType.Integer && configType != ConfigType.Decimal)
                        {
                            if (!string.IsNullOrWhiteSpace(min))
                            {
                                throw new ArgumentException(
                                    $"Non-numeric config item cannot have the 'min' attribute configured. Hint:'{GetHint()}'");
                            }
                        }

                        // 如果未配置 min，则取最小值
                        if (string.IsNullOrWhiteSpace(min))
                        {
                            if (configType == ConfigType.Decimal)
                            {
                                min = double.MinValue.ToString(CultureInfo.InvariantCulture);
                            }
                            else if (configType == ConfigType.Integer)
                            {
                                min = long.MinValue.ToString(CultureInfo.InvariantCulture);
                            }
                        }
                        else
                        {
                            min = min.Trim();
                            if ((configType == ConfigType.Decimal && !StringParser.TryParse2Double(min, out _))
                                || (configType == ConfigType.Integer && !StringParser.TryParse2Long(min, out _)))
                            {
                                throw new ArgumentException(
                                    $"Invalid value for 'min' attribute: '{min}'. Hint:'{GetHint()}'");
                            }
                        }

                        // max
                        var max = xmlReader.GetAttribute("max");
                        if (configType != ConfigType.Integer && configType != ConfigType.Decimal)
                        {
                            if (!string.IsNullOrWhiteSpace(max))
                            {
                                throw new ArgumentException(
                                    $"Non-numeric config item cannot have the 'max' attribute configured. Hint:'{GetHint()}'");
                            }
                        }

                        // 如果未配置 max，则取最小值
                        if (string.IsNullOrWhiteSpace(max))
                        {
                            if (configType == ConfigType.Decimal)
                            {
                                max = double.MaxValue.ToString(CultureInfo.InvariantCulture);
                            }
                            else if (configType == ConfigType.Integer)
                            {
                                max = long.MaxValue.ToString(CultureInfo.InvariantCulture);
                            }
                        }
                        else
                        {
                            max = max.Trim();
                            if ((configType == ConfigType.Decimal && !StringParser.TryParse2Double(max, out _))
                                || (configType == ConfigType.Integer && !StringParser.TryParse2Long(max, out _)))
                            {
                                throw new ArgumentException(
                                    $"Invalid value for 'max' attribute: '{max}'. Hint:'{GetHint()}'");
                            }
                        }

                        // regex
                        regex = xmlReader.GetAttribute("regex");
                        if (regex != null)
                        {
                            regex = regex.Trim();
                        }

                        // regexNote
                        regexNote = xmlReader.GetAttribute("regex_note");
                        if (regexNote != null)
                        {
                            regexNote = regexNote.Trim();
                            if (string.IsNullOrWhiteSpace(regexNote))
                            {
                                regexNote = regex;
                            }
                        }

                        // options
                        List<string> options = new List<string>();
                        string optionsText = xmlReader.GetAttribute("options");
                        if (optionsText != null)
                        {
                            optionsText = optionsText.Trim();
                            if (!string.IsNullOrWhiteSpace(optionsText))
                            {
                                if (optionsText.Split(';').Any(x => string.IsNullOrWhiteSpace(x)))
                                {
                                    throw new ArgumentException(
                                        $"Comma cannot appear at both ends, and it cannot appear consecutively. Hint:'{GetHint()}'");
                                }

                                options = optionsText.Split(';').Select(o => o.Trim()).ToList();
                                if (configType == ConfigType.Bool)
                                {
                                    if (options.Count != 2)
                                    {
                                        throw new ArgumentException(
                                            $"The 'options' attribute must contain only two options for boolean type. Hint:'{GetHint()}'");
                                    }
                                }
                            }
                            else
                            {
                                if (configType == ConfigType.Bool)
                                {
                                    options.Add("Yes");
                                    options.Add("No");
                                }
                            }
                        }
                        else
                        {
                            if (configType == ConfigType.Bool)
                            {
                                options.Add("Yes");
                                options.Add("No");
                            }
                        }
                        // unit
                        unit = xmlReader.GetAttribute("unit");
                        if (unit != null)
                        {
                            unit = unit.Trim();
                        }

                        // restart
                        restart = xmlReader.GetAttribute("restart");
                        if (restart != null)
                        {
                            restart = restart.Trim();
                            if (!string.IsNullOrWhiteSpace(restart))

                            {
                                if (!bool.TryParse(restart, out _))
                                {
                                    throw new ArgumentException(
                                        $"The 'restart' attribute must be a boolean value. Hint:'{GetHint()}'");
                                }
                            }
                        }

                        // initialValue
                        var initialValue = xmlReader.GetAttribute("initial_value");
                        initialValue = initialValue.Trim();
                        if (configType == ConfigType.Bool)
                        {
                            if (!bool.TryParse(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a boolean value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.Integer)
                        {
                            if (!StringParser.TryParse2Long(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a integer value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.Decimal)
                        {
                            if (!StringParser.TryParse2Double(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a numeric value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.File)
                        {
                            if (!StringParser.TryParse2File(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a file value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.Folder)
                        {
                            if (!StringParser.TryParse2Directory(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a folder value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.Color)
                        {
                            if (!StringParser.TryParse2Color(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a color value. Hint:'{GetHint()}'");
                            }
                        }
                        else if (configType == ConfigType.DateTime)
                        {
                            if (!StringParser.TryParse2DateTime(initialValue, out _))
                            {
                                throw new ArgumentException(
                                    $"The 'value' attribute must be a DateTime value. Hint:'{GetHint()}'");
                            }
                        }

                        var node = nodeStack.Peek();
                        if (node.ConfigItems.Any(c => c.Name == name))
                        {
                            throw new ArgumentException(
                                $"The 'name' attribute must be unique within the same node's configItems. Hint:'{GetHint()}'");
                        }

                        node.ConfigItems.Add(new ConfigItem()
                        {
                            Name = name,
                            Display = display,
                            Description = description,
                            Visible = bool.Parse(visible),
                            Type = configType,
                            Enable = bool.Parse(enable),
                            MinValue = min,
                            MaxValue = max,
                            Unit = unit,
                            StringValue = initialValue,
                            ObjectValue = Convert2Object(configType, initialValue),
                            Options = options.ToArray(),
                            Restart = bool.Parse(restart),
                            Regex = regex,
                            RegexNote = regexNote,
                        });
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"Element must be either a node (only allowed attributes: name, display, visible, enable) or a config item (only allowed attributes: name, display, type, initial_value, desc, min, max, options, unit, visible, enable, restart, regex, regex_note). Hint:'{GetHint()}'");
                    }
                }
                else if (xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    if (nodeStack.Count > 0)
                        nodeStack.Pop();
                }
            }
            foreach (var rootNode in _rootNodes)
            {
                Travel(rootNode);
            }
            RootNodes = _rootNodes.ToArray();
        }

        private Dictionary<string, string> ReadConfigCurrentValues()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            return keyValues;
        }
        private void Travel(ConfigNode configNode)
        {
            foreach (var item in configNode.ConfigItems)
            {
                string configPath = configNode.Path + "." + item.Name;

                var options = CustomizeOptionsSource(this,configPath);
                if (options != null && options.Length > 0)
                {
                    item.Options = options;
                }

                item.ValidationRule = new Action<string>((string text) => { ValidateValue(configPath, text); });
            }

            foreach (var node in configNode.Children)
            {
                Travel(node);
            }
        }

    }
}