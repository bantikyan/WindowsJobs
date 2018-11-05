using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Models
{
    public class JobModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Type JobType { get; set; }
        public int Interval { get; set; }
        public int DailyAtHour { get; set; }
        public int DailyAtMinute { get; set; }
    }
}
