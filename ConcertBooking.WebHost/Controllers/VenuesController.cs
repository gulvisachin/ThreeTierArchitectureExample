using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.WebHost.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IVenue _venueRepo;
        [BindProperty]
        public VenueVM venueVM { get; set; }
        public VenuesController(IVenue venueRepo)
        {
            _venueRepo = venueRepo;
            venueVM = new VenueVM()
            {
                venue = new Venue(),
                venues = new List<Venue>()
            };
        }

        public async Task<IActionResult> AllVenues()
        {
            var lstVenues = await _venueRepo.GetAllAsych();
            return Json(new { data = lstVenues ?? null });
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id == null || id == 0)
            {
                return View(venueVM);
            }
            else
            {
                venueVM.venue = await _venueRepo.GetByIdAsych(id);
                if (venueVM.venue == null || venueVM.venue.Id == 0) return NotFound();
                else
                    return View(venueVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate()
        {
            if (ModelState.IsValid)
            {
                if (venueVM.venue != null && venueVM.venue.Id > 0)
                {
                    var venueById = await _venueRepo.GetByIdAsych(venueVM.venue.Id);
                    if (venueById == null) return NotFound();

                    await _venueRepo.Update(venueVM.venue);
                    TempData["Success"] = "Venue Updated done !";
                }
                else
                {
                    await _venueRepo.Add(venueVM.venue);
                    TempData["Success"] = "Venue inserted done !";
                }
            }
            else
            {
                TempData["Error"] = "Something went wrong !";
                return View(venueVM);
            }

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var venueById = await _venueRepo.GetByIdAsych(id);
            if (venueById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _venueRepo.Delete(venueById);

                return Json(new { success = true, message = "Venue deleted done." });
            }
        }
        #endregion
    }
}
