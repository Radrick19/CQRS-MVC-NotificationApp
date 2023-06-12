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
    public class DailyTaskRepository : BaseRepository<OnetimeTask>
    {
        public DailyTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<OnetimeTask> Get()
        {
            return _dbSet.Include(task => task.User);
        }

        public override async Task<OnetimeTask> GetByIdAsync(int entityId)
        {
            return await _dbSet.Include(task => task.User).FirstOrDefaultAsync(task=> task.Id == entityId);
        }
    }
}
