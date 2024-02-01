using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers.Shopping
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TestingContext _context;
        CategoryVM categoryVM = new CategoryVM();
        public CategoryController(IUnitOfWork unitOfWork, TestingContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        //public IActionResult AllCategories()
        //{
        //    var lstCategory = _unitOfWork.Category.GetAll();
        //    return Json(new { data = lstCategory ?? null });
        //}
        public async Task<IActionResult> AllCategories()
        {
            var lstCategory = await _context.Categories.ToListAsync();
            return Json(new { data = lstCategory ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int? id)
        {
            if (id != 0)
            {
                categoryVM.Category = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(CategoryVM model)
        {
            if (model.Category != null && model.Category.Id > 0)
            {
                var categoryById = await _context.Categories.Where(x => x.Id == model.Category.Id).FirstOrDefaultAsync();
                if (categoryById == null) return NotFound();

                _context.Categories.Update(model.Category);
                TempData["Success"] = "Category Updated done !";
            }
            else
            {
                await _context.Categories.AddAsync(model.Category);
                TempData["Success"] = "Category inserted done !";
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var CategoryById = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (CategoryById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                _context.Categories.Remove(CategoryById);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Category deleted done." });
            }
        }
        #endregion
    }
}
