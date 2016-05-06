namespace Troubadour.Models
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private TroubadourContext _context;

        public UnitOfWorkFactory(TroubadourContext context)
        {
            _context = context;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_context);
        }
    }
}
