namespace SCADA.Configuration
{
    public partial class PrimitiveConfigSource
    {
        public ConfigNode GetConfigNode(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && RootNodes != null && RootNodes.Length > 0)
            {
                foreach (var node in RootNodes)
                {
                    if (ConfigNode.Find(path, false, node, out _, out ConfigNode configNode))
                    {
                        return configNode;
                    }
                }
            }
            return null;
        }
    }
}