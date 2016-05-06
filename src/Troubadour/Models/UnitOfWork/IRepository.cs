using System;
using System.Linq;
using System.Linq.Expressions;

namespace Troubadour.Models
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}