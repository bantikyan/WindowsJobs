using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetalex.WindowsJobs.Jobs
{
    [DisallowConcurrentExecution]
    public abstract class BaseJob : IDisposable
    {
        protected Autofac.ILifetimeScope lifetimeScope;
        protected DAL.IUnitOfWork unitOfWork;
        protected NLog.Logger Logger;

        public BaseJob(Autofac.ILifetimeScope lifetimeScope, DAL.IUnitOfWork unitOfWork)
        {
            this.lifetimeScope = lifetimeScope;
            this.unitOfWork = unitOfWork;
            this.Logger = NLog.LogManager.GetLogger(GetType().Name);
        }

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
