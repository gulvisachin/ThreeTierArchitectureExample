using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;
using ThreeTierArch.UI.ViewModels;

namespace ThreeTierArch.UI.Controllers
{
    public class StatesController : Controller
    {
        private readonly IState _stateRepo;
        private readonly ICountry _countryRepo;
        //State state = new State();
        [BindProperty]
        StateVM stateVM { set; get; }
        CreateStateVM createStateVM =new CreateStateVM();
        public StatesController(IState stateRepo, ICountry countryRepo)
        {
            _stateRepo = stateRepo;
            _countryRepo = countryRepo;
            stateVM = new StateVM()
            {
                state = new State(),
                states = new List<State>(),
                countries = new List<Country>()
            };

        }
        public async Task<IActionResult> Allstates()
        {
            var lststates = await _stateRepo.GetAllAsych();
            return Json(new { data = lststates ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id != 0)
            {
                var state = await _stateRepo.GetByIdAsych(id);
                if (state == null || state.Id == 0) return NotFound();

                createStateVM = new CreateStateVM
                {
                    Id = state.Id,
                    Name = state.Name,
                    CountryId = state.CountryId,
                };
            }

            var countries = await _countryRepo.GetAllAsych();
            ViewBag.CountryList = new SelectList(countries, "Id", "Name");

            return View(createStateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(CreateStateVM createStateVM)
        {
            if (ModelState.IsValid)
            {
                if (createStateVM != null && createStateVM.Id > 0)
                {
                    var stateById = await _stateRepo.GetByIdAsych(createStateVM.Id);
                    if (stateById == null) return NotFound();
                    var state = BindToView(createStateVM);
                    await _stateRepo.Update(state);
                    TempData["Success"] = "State Updated done !";
                }
                else
                {
                    var state = BindToView(createStateVM);
                    await _stateRepo.Add(state);
                    TempData["Success"] = "State inserted done !";
                }
            }
            else
            {
                TempData["Error"] = "Something went wrong !";
                return View(stateVM);
            }
            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var stateById = await _stateRepo.GetByIdAsych(id);
            if (stateById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _stateRepo.Delete(stateById);
                //await _context.SaveChangesAsync();

                return Json(new { success = true, message = "State deleted done." });
            }
        }
        #endregion

        private State BindToView(CreateStateVM createStateVM)
        {
            var state = new State
            {
                Id = createStateVM.Id,
                Name = createStateVM.Name,
                CountryId = createStateVM.CountryId,
            };
            return state;
        }
    }
}
