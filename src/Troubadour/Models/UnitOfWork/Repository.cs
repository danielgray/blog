using System;
using System.Linq;
using System.Linq.Expressions;

namespace Troubadour.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TroubadourContext _context;

        public Repository(TroubadourContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Add(T entity)
        {
            //if (entity is IBaseModel)
            //{
            //    ((IBaseModel)entity).DateCreated = DateTime.UtcNow;
            //}

            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
