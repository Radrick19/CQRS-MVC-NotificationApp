using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        FastScheduleContext GetContext();

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task SaveChangesAsync();
    }
}
