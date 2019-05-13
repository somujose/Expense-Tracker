using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpenseTracker.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        void Delete(T entity);
        void Edit(T Entity);

    }
}
