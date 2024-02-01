using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICity _cityRepo;
        private readonly IState _stateRepo;

        City city = new City();
        public CitiesController(ICity cityRepo, IState stateRepo)
        {
            _cityRepo = cityRepo;
            _stateRepo = stateRepo;
        }
        public async Task<IActionResult> AllCities()
        {
            var lstcities = await _cityRepo.GetAllAsync();
            return Json(new { data = lstcities ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id != 0)
            {
                city = await _cityRepo.GetByIdAsync(id);
            }

            var states = await _stateRepo.GetAllAsych();
            ViewBag.StateList = new SelectList(states, "Id", "Name");
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(City model)
        {
            if (model != null && model.Id > 0)
            {
                var stateById = await _cityRepo.GetByIdAsync(model.Id);
                if (stateById == null) return NotFound();

                await _cityRepo.Update(model);
                TempData["Success"] = "City Updated done !";
            }
            else
            {
                await _cityRepo.Add(model);
                TempData["Success"] = "City inserted done !";
            }
            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var stateById = await _cityRepo.GetByIdAsync(id);
            if (stateById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _cityRepo.Delete(stateById);
                //await _context.SaveChangesAsync();

                return Json(new { success = true, message = "City deleted done." });
            }
        }
        #endregion
    }
}
