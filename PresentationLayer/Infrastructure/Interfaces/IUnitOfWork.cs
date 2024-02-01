using PresentationLayer.Infrastructure.Interfaces.Shopping;

namespace PresentationLayer.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IProduct Product { get; }
        void Save();
    }
}
