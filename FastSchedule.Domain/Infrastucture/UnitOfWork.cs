using FastSchedule.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Infrastucture
{
    public class UnitOfWork : IUnitOfWork
    {
        private FastScheduleContext dbContext;
        private bool disposed = false;

        public UnitOfWork(FastScheduleContext context)
        {
            dbContext = context;
        }

        public async Task BeginTransactionAsync()
        {
           await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await dbContext.Database.RollbackTransactionAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task DisposeAsync(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    await dbContext.DisposeAsync();
                }
                disposed = true;
            }
        }

        public FastScheduleContext GetContext()
        {
            if(disposed)
            {
                dbContext = new FastScheduleContext();
            }
            return dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
