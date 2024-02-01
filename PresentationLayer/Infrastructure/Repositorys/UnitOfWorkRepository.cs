//using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces;
using PresentationLayer.Infrastructure.Interfaces.Shopping;
using PresentationLayer.Infrastructure.Repositorys.Shopping;

namespace PresentationLayer.Infrastructure.Repositorys
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        //private readonly TestingContext _context;

        public ICategory Category { get; private set; }
        public IProduct Product { get; private set; }
        //public UnitOfWorkRepository(TestingContext context)
        //{
        //    _context = context;
        //    Category = new CategoryRepository(_context);
        //    Product = new ProductRepository(context);
        //}
        public void Save()
        {
            //_context.SaveChanges();
        }
    }
}
