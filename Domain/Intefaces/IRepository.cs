using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Intefaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeExpressions);

    }
}
