using Autofac;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Jobs
{
    public class TestJob : BaseJob, IJob
    {
        public TestJob(ILifetimeScope lifetimeScope, DAL.IUnitOfWork unitOfWork)
            :base(lifetimeScope, unitOfWork)
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.Info("HELLO!");

            try
            {
               
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Logger.Error(ex);
            }

            await Console.Out.WriteLineAsync("DONE!");
            await Task.Delay(50000);
        }
    }
}
