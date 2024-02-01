using Microsoft.AspNetCore.Mvc;
//using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces;

namespace PresentationLayer.Controllers.Shopping
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            //productVM = new ProductVM()
            //{
            //    product = new Product(),
            //    categories = _unitOfWork.Category.GetAll()
            //};
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult AllProducts()
        {
            var products = _unitOfWork.Product.GetAll(null,null);
            return Json(new { data = products });

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
