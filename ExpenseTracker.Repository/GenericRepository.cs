using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace ExpenseTracker.Repository
{
    public abstract class GenericRepository<T> :IGenericRepository<T>
        where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
             _dbset.Remove(entity);
        }

        public void Edit(T Entity)
        {
            _entities.Entry(Entity).State = EntityState.Modified;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
           return _dbset.Where(predicate).AsEnumerable();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable();
        }

    }
}
