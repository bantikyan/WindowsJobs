using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Logging
{
    class NLogProvider : Quartz.Logging.ILogProvider
    {
        protected NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Quartz.Logging.Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= Quartz.Logging.LogLevel.Info && func != null)
                {
                    Logger.Info("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
