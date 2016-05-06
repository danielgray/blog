namespace Troubadour.Models
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
