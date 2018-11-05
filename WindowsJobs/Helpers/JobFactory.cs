using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetalex.WindowsJobs.Helpers;
using Zetalex.WindowsJobs.Models;

namespace Zetalex.WindowsJobs.Helpers
{
    public static class JobFactory
    {
        public static List<JobModel> GetAllJobs()
        {
            List<JobModel> lst = new List<JobModel>();

            foreach (JobEnum item in Enum.GetValues(typeof(JobEnum)))
            {
                //Job details
                JobModel jobModel = new JobModel();
                jobModel.Number = item.GetHashCode();
                jobModel.Name = "job" + jobModel.Number;
                jobModel.JobType = Type.GetType("Zetalex.WindowsJobs.Jobs." + item.ToString());

                //Get Interval from settings
                var attr = GetAttr(item);
                //if (Helpers.SettingManager.DicItems.ContainsKey(item.ToString()))
                //{
                //    var sTime = Helpers.SettingManager.DicItems[item.ToString()].ToString();

                //    TimeSpan tTime;
                //    int dInterval;

                //    if (int.TryParse(sTime, out dInterval))
                //    {
                //        //Skip job if interval equal -1
                //        if (dInterval == -1)
                //        {
                //            continue;
                //        }

                //        //
                //        jobModel.Interval = dInterval;
                //    }
                //    else if(TimeSpan.TryParse(sTime, out tTime))
                //    {
                //        jobModel.DailyAtHour = tTime.Hours;
                //        jobModel.DailyAtMinute = tTime.Minutes;
                //    }
                //}
                //Get Interval from enum
                //else 
                if (attr != null)
                {
                    jobModel.Interval = attr.Interval;

                    jobModel.DailyAtHour = attr.DailyAtHour;
                    jobModel.DailyAtMinute = attr.DailyAtMinute;
                }

                //check 
                if (jobModel.Interval != 0 || jobModel.DailyAtHour != 0 || jobModel.DailyAtMinute != 0)
                {
                    lst.Add(jobModel);
                }
            }

            return lst;
        }

        private static JobStateAttribute GetAttr(JobEnum p)
        {
            var field = typeof(JobEnum).GetField(Enum.GetName(typeof(JobEnum), p));
            var attr = (JobStateAttribute)Attribute.GetCustomAttribute(field, typeof(JobStateAttribute));

            return attr;
        }
    }
}
