using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Implementions;
using ThreeTierArch.Repositories.Interfaces;
using ThreeTierArch.UI.ViewModels;

namespace ThreeTierArch.UI.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountry _countryRepo;
        //Country country = new Country();
        [BindProperty]
        public CountryVM countryVM { get; set; }
        public CountriesController(ICountry countryRepo)
        {
            _countryRepo = countryRepo;
            countryVM = new CountryVM()
            {
                country = new Country(),
                countries = new List<Country>()
            };
        }

        public async Task<IActionResult> AllCountries()
        {
            var lstCountries = await _countryRepo.GetAllAsych();
            return Json(new { data = lstCountries ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id == null || id == 0)
            {
                return View(countryVM);
            }
            else
            {
                countryVM.country = await _countryRepo.GetByIdAsych(id);
                if (countryVM.country == null || countryVM.country.Id == 0) return NotFound();
                else
                    return View(countryVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate()
        {
            if (ModelState.IsValid)
            {
                if (countryVM.country != null && countryVM.country.Id > 0)
                {
                    var countryById = await _countryRepo.GetByIdAsych(countryVM.country.Id);
                    if (countryById == null) return NotFound();

                    await _countryRepo.Update(countryVM.country);
                    TempData["Success"] = "Country Updated done !";
                }
                else
                {
                    await _countryRepo.Add(countryVM.country);
                    TempData["Success"] = "Country inserted done !";
                }
            }
            else
            {
                TempData["Error"] = "Something went wrong !";
                return View(countryVM);
            }
            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var countryById = await _countryRepo.GetByIdAsych(id);
            if (countryById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _countryRepo.Delete(countryById);
                //await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Country deleted done." });
            }
        }
        #endregion
    }
}
