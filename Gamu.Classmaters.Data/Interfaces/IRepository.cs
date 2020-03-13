using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gamu.Classmaters.Data.Interfaces
{
    public interface IRepository<T> where T: IRepositoryEntity
    {
        IList<T> AllItems { get; }

        IList<T> GetItems(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Delete(T entity);

        void Add(T entity);
    }
}
