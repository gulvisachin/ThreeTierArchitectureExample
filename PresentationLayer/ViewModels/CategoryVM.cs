using PresentationLayer.DataAccessLayer;

namespace PresentationLayer.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; } = new Category();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
