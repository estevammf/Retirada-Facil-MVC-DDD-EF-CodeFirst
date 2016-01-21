using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace APP.StoreManager.Application.Interface
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
        void Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        void Dispose();
    }
}
