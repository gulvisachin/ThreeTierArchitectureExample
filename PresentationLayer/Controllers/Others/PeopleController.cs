using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.DataAccessLayer;
using PresentationLayer.Infrastructure.Interfaces;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers.Others
{
    public class PeopleController : Controller
    {
        private readonly TestingContext _context;
        PeopleVM peopleVM = new PeopleVM();
        public PeopleController(TestingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AllPeoples()
        {
            var result = await _context.TblPeople.ToListAsync();
            return Json(new { data = result ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int? id)
        {
            if(id != 0)
            {
                peopleVM.People = await _context.TblPeople.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            return View(peopleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(PeopleVM model)
        {
            if (model.People != null && model.People.Id > 0)
            {
                var categoryById = await _context.TblPeople.Where(x => x.Id == model.People.Id).FirstOrDefaultAsync();
                if (categoryById == null) return NotFound();

                _context.TblPeople.Update(model.People);
                TempData["Success"] = "People Updated done !";
            }
            else
            {
                await _context.TblPeople.AddAsync(model.People);
                TempData["Success"] = "People inserted done !";
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var peopleById = await _context.TblPeople.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (peopleById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                 _context.TblPeople.Remove(peopleById);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "People deleted done." });
            }
        }
        #endregion
    }
}
