using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DronesWebApi.Persistence.Repositories;

namespace DronesWebApi.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(params object[] keyObjects);
        IPaginatedList<TEntity> GetPaginated(int pageIndex, int pageSize);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}