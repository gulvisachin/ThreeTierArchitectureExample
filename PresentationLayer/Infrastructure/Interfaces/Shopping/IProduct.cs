using PresentationLayer.DataAccessLayer;

namespace PresentationLayer.Infrastructure.Interfaces.Shopping
{
    public interface IProduct : ICommon<Product>
    {
        void Update(Product entity);
    }
}
