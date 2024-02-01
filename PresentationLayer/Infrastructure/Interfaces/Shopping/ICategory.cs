using PresentationLayer.DataAccessLayer;

namespace PresentationLayer.Infrastructure.Interfaces.Shopping
{
    public interface ICategory : ICommon<Category>
    {
        void Update(Category category);
    }
}
