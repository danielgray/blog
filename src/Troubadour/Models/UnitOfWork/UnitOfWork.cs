namespace Troubadour.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        protected TroubadourContext _context;

        public UnitOfWork(TroubadourContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
