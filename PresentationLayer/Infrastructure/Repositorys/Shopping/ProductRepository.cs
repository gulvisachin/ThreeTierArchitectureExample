using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces.Shopping;

namespace PresentationLayer.Infrastructure.Repositorys.Shopping
{
    public class ProductRepository : CommonRepository<Product>, IProduct
    {
        private readonly TestingContext _contex;
        public ProductRepository(TestingContext contex) : base(contex)
        {
            _contex = contex;
        }
        public void Update(Product entity)
        {
            var product = _contex.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Price = entity.Price;
                if (product.ImageUrl != null)
                {
                    product.ImageUrl = entity.ImageUrl;
                }
                _contex.Products.Update(product);
            }
        }
    }
}
