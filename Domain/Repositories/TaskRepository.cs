using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Repositories
{
    public class TaskRepository : BaseRepository<ScheduleTask>
    {
        public TaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<ScheduleTask> Get()
        {
            return _dbSet.Include(task => task.User);
        }

        public override async Task<ScheduleTask> GetByIdAsync(int entityId)
        {
            return await _dbSet.Include(task => task.User).FirstOrDefaultAsync(task=> task.Id == entityId);
        }
    }
}
