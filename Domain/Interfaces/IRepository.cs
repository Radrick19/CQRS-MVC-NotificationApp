using FastSchedule.Domain.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public IEnumerable<TEntity> Get();

        public Task<TEntity> GetByIdAsync(int id);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> func);

        public Task AddAsync(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

    }
}
