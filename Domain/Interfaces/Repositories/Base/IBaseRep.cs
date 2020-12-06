using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IBaseRep<TEntity, TId>
        where TEntity : BaseEntity
        where TId : struct
    {
        TEntity Create(TEntity entity);
        void Create(IEnumerable<TEntity> data);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
        void Delete(IEnumerable<TEntity> entities);
        bool Exists(Func<TEntity, bool> where);
        TEntity GetById(TId id, bool tracking = true);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, bool tracking = true);
        IQueryable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> where, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> GetAndOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, bool asc = true, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetBy(Func<TEntity, bool> where, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> GetAll(bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> GetAndOrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool asc = true, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(TId id, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties);
        void SaveChanges();
    }
}
