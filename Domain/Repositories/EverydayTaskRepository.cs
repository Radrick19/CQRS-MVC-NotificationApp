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
    public class EverydayTaskRepository : BaseRepository<EverydayTask>
    {
        public EverydayTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<EverydayTask> Get()
        {
            return _dbSet.Include(task => task.User);
        }

        public override async Task<EverydayTask> GetByIdAsync(int entityId)
        {
            return await _dbSet.Include(task => task.User).FirstOrDefaultAsync(task => task.Id == entityId);
        }
    }
}
