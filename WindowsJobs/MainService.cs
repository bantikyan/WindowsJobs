using Autofac;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Zetalex.WindowsJobs.Helpers;

namespace Zetalex.WindowsJobs
{
    public partial class MainService : ServiceBase
    {
        private NLog.Logger Logger;
        private IScheduler scheduler;

        public MainService()
        {
            InitializeComponent();

            Logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected async override void OnStart(string[] args)
        {
            try
            {
                //Configure Container
                IContainer container = Helpers.IocConfig.ConfigureContainer();

                //Configure LogProvider
                //Quartz.Logging.LogProvider.SetCurrentLogProvider(new Logging.NLogProvider());
                Quartz.Logging.LogProvider.SetCurrentLogProvider(new Logging.ConsoleLogProvider());

                //StdSchedulerFactory factory = new StdSchedulerFactory();
                ISchedulerFactory factory = container.Resolve<ISchedulerFactory>();
                scheduler = await factory.GetScheduler();

                //Start scheduler
                await scheduler.Start();

                //Get All Jobs
                var lstJobs = JobFactory.GetAllJobs();

                foreach (var jobItem in lstJobs)
                {
                    //Define the job 
                    IJobDetail job = JobBuilder.Create(jobItem.JobType)
                        .WithIdentity(jobItem.Name, "groupTools")
                        .Build();

                    //Trigger the job to run now, and then repeat by Interval
                    ITrigger trigger = null;
                    if (jobItem.Interval != 0)
                    {
                        trigger = TriggerBuilder.Create()
                            .WithIdentity("trigger" + jobItem.Number, "groupTools")
                            .StartNow()
                            .WithSimpleSchedule(x => x
                                .WithIntervalInSeconds(jobItem.Interval)
                                .RepeatForever())
                            .Build();
                    }
                    else
                    {
                        trigger = TriggerBuilder.Create()
                            .WithIdentity("trigger" + jobItem.Number, "groupTools")
                            .StartNow()
                            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(jobItem.DailyAtHour, jobItem.DailyAtMinute))
                            .Build();
                    }

                    //Tell quartz to schedule the job using our trigger
                    await scheduler.ScheduleJob(job, trigger);
                }
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            Logger.Info("Service Started");
        }

        protected async override void OnStop()
        {
            //Shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();

            //Write scheduler MetaData
            SchedulerMetaData metaData = await scheduler.GetMetaData();
            Logger.Info("Executed " + metaData.NumberOfJobsExecuted + " jobs.");

            Logger.Info("Service Stopped");
        }
    }
}
