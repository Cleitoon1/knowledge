using Domain.Interfaces.Repositories.Base;
using Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories.Base
{
    public class BaseRep<TEntity, TId> : IBaseRep<TEntity, TId>
        where TEntity : BaseEntity
        where TId : struct
    {
        protected KnowledgeContext _context;

        public BaseRep(KnowledgeContext context)
        {
            _context = context;
        }

        public TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public void Create(IEnumerable<TEntity> data)
        {
            _context.Set<TEntity>().AddRange(data);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TId id)
        {
            _context.Set<TEntity>().Remove(GetById(id));
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public bool Exists(Func<TEntity, bool> where)
        {
            return _context.Set<TEntity>().Any(where);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, bool tracking = true)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetAll(bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includeProperties.Any())
                return Include(_context.Set<TEntity>(), includeProperties);
            return query;
        }

        public IQueryable<TEntity> GetAndOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> orderBy, bool asc = true, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return asc ? GetAllBy(where, tracking, includeProperties).OrderBy(orderBy) : GetAllBy(where, tracking, includeProperties).OrderByDescending(orderBy);
        }

        public IQueryable<TEntity> GetAndOrderBy<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool asc = true, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return asc ? GetAll(tracking, includeProperties).OrderBy(orderBy) : GetAll(tracking, includeProperties).OrderByDescending(orderBy);

        }

        public IQueryable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> where, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAll(tracking, includeProperties).Where(where);
        }

        public TEntity GetBy(Func<TEntity, bool> where, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAll(tracking, includeProperties).Where(where).FirstOrDefault();
        }

        public TEntity GetById(TId id, bool tracking = true)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity GetById(TId id, bool tracking = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAll(tracking, includeProperties).Where(x => x.Id.ToString() == id.ToString()).FirstOrDefault();
        }

        private IQueryable<TEntity> Include(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
                query = query.Include(property);
            return query;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
