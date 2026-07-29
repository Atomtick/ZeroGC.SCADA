using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Atomtick.Configuration
{
    public sealed class ConfigNode
    {
        public ConfigNode()
        {
            Children = new List<ConfigNode>();
            ConfigItems = new List<ConfigItem>();
        }

        public IReadOnlyList<ConfigNode> Children { get; }
        public IReadOnlyList<ConfigItem> ConfigItems { get; }
        public string Display { get; internal set; }
        public bool Enable { get; internal set; }
        public bool IsLeaf => Children.Count == 0;
        public bool IsRoot => Parent == null;
        public string Name { get; internal set; }
        public ConfigNode Parent { get; internal set; }
        public string Path => IsRoot ? Name : Parent.Path + "." + Name;
        public bool Visible { get; internal set; }

        public static bool FindByItemPath(string itemPath, IEnumerable<ConfigNode> nodes, out ConfigItem configItem, out ConfigNode configNode)
        {
            foreach (var node in nodes)
            {
                if (FindByItemPath(itemPath, node, out configItem, out configNode))
                {
                    return true;
                }
            }
            configItem = null;
            configNode = null;
            return false;
        }

        public static bool FindByItemPath(string itemPath, ConfigNode node, out ConfigItem configItem, out ConfigNode configNode)
        {
            return Find(itemPath, true, node, out configItem, out configNode);
        }

        public static bool FindByNodePath(string nodePath, ConfigNode node, out ConfigNode configNode)
        {
            return Find(nodePath, false, node, out _, out configNode);
        }

        public static bool FindByNodePath(string nodePath, IEnumerable<ConfigNode> nodes, out ConfigNode configNode)
        {
            foreach (var node in nodes)
            {
                if (FindByNodePath(nodePath, node, out configNode))
                {
                    return true;
                }
            }
            configNode = null;
            return false;
        }

        public static bool Find(string path, bool isTrailConfigItem, ConfigNode node, out ConfigItem configItem, out ConfigNode configNode)
        {
            var names = path.Split('.');
            ConfigNode result = null;
            if (isTrailConfigItem)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    if (i == names.Length - 1)
                    {
                        break;
                    }

                    if (node == null)
                    {
                        configNode = null;
                        configItem = null;
                        return false;
                    }

                    if (node.Name.Equals(names[i]))
                    {
                        result = node;
                        if (i < names.Length - 1)
                            node = node.Children.FirstOrDefault(x => x.Name.Equals(names[i + 1]));
                    }
                    else
                    {
                        configNode = null;
                        configItem = null;
                        return false;
                    }
                }
                if (result == null)
                {
                    configNode = null;
                    configItem = null;
                    return false;
                }
                configItem = result.ConfigItems.FirstOrDefault(x => x.Name == names[names.Length - 1]);
                if (configItem != null)
                {
                    configNode = result;
                    return true;
                }
                configNode = null;
                configItem = null;
                return false;
            }
            else
            {
                configItem = null;
                for (int i = 0; i < names.Length; i++)
                {
                    if (node == null)
                    {
                        configNode = null;
                        return false;
                    }

                    if (node.Name.Equals(names[i]))
                    {
                        result = node;
                        if (i < names.Length - 1)
                        {
                            node = node.Children.FirstOrDefault(x => x.Name.Equals(names[i + 1]));
                        }
                    }
                    else
                    {
                        configNode = null;
                        return false;
                    }
                }
            }
            configNode = result;
            return true;
        }
    }
}
