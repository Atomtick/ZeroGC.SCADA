using NLog;
using NLog.Config;

namespace SCADA.DiskLogger
{
    public class LogManager
    {
        public LogManager()
        {
            Logger d = LogManager.GetCurrentClassLogger();
        }

        public static Logger GetCurrentClassLogger()
        {
            LoggingConfiguration config = XmlLoggingConfiguration.CreateFromXmlString("");
            LogFactory factory = new LogFactory();
            return NLog.LogManager.GetCurrentClassLogger();
        }

        public static Logger GetLogger(string name)
        {
            return NLog.LogManager.GetLogger(name); ;
        }
    }
}