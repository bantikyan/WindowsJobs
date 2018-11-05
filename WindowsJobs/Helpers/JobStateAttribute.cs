using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class JobStateAttribute : System.Attribute
    {
        public int Interval { get; set; }
        public int DailyAtHour { get; set; }
        public int DailyAtMinute { get; set; }
        public JobStateAttribute(int interval = 0, int dailyAtHour = 0, int dailyAtMinute = 0)
        {
            Interval = interval;
            DailyAtHour = dailyAtHour;
            DailyAtMinute = dailyAtMinute;
        }
    }
}
