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
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = context.Set<TEntity>();
        }


        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _dbSet.FirstOrDefaultAsync(func);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

        public virtual async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
