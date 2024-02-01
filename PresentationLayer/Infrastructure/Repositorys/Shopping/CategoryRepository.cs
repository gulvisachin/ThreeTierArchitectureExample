using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces.Shopping;

namespace PresentationLayer.Infrastructure.Repositorys.Shopping
{
    public class CategoryRepository : CommonRepository<Category>, ICategory
    {
        private readonly TestingContext _context;

        public CategoryRepository(TestingContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryDB = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (categoryDB != null)
            {
                categoryDB.Name = category.Name;
                categoryDB.DisplayOrder = category.DisplayOrder;
                _context.Categories.Update(categoryDB);
            }
        }
    }
}
