using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private FastScheduleContext context => _unitOfWork.GetContext();
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = context.Set<TEntity>();
        }


        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _dbSet.FirstOrDefaultAsync(func);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
