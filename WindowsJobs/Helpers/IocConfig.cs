using Autofac;
using Autofac.Extras.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Helpers
{
    public static class IocConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Register Jobs
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(Jobs.TestJob).Assembly));
            
            //Register Services and UnitOfWork
            builder.RegisterType<DAL.UnitOfWork>().As<DAL.IUnitOfWork>(); 
            
            return builder.Build();
        }
    }
}
