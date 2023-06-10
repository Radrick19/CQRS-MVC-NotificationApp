using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Repositories
{
    public class MonthlyTaskRepository : BaseRepository<MonthlyTask>
    {
        public MonthlyTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<MonthlyTask> Get()
        {
            return _dbSet.Include(task => task.User);
        }

        public override async Task<MonthlyTask> GetByIdAsync(int entityId)
        {
            return await _dbSet.Include(task => task.User).FirstOrDefaultAsync(task => task.Id == entityId);
        }
    }
}
