using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Logging
{
    public class ConsoleLogProvider : Quartz.Logging.ILogProvider
    {
        public Quartz.Logging.Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= Quartz.Logging.LogLevel.Info && func != null)
                {
                    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
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
