using AutoMapper;
using ConcertBooking.Entities;
using ConcertBooking.Repositories.Implementions;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ConcertBooking.WebHost.Controllers
{
    public class ConcertsController : Controller
    {
        private readonly IConcert _concertRepo;
        private readonly IArtist _artistRepo;
        private readonly IVenue _venueRepo;
        private readonly IUtility _utilityRepo;
        private readonly IBooking _bookingRepo;
        private readonly IMapper _mapper;
        private string ContainerName = "ConcertImage";
        [BindProperty]
        ConcertVM concertVM { set; get; }
        CreateConcertVM createConcertVM = new CreateConcertVM();
        public ConcertsController(IConcert stateRepo, IArtist artistRepo, IVenue venueRepo, IUtility utilityRepo, IBooking bookingRepo, IMapper mapper)
        {
            _concertRepo = stateRepo;
            _artistRepo = artistRepo;
            _venueRepo = venueRepo;
            _utilityRepo = utilityRepo;
            _mapper = mapper;

            concertVM = new ConcertVM()
            {
                concert = new Concert(),
                concerts = new List<Concert>(),
            };
            _bookingRepo = bookingRepo;
        }
        public async Task<IActionResult> AllConcerts()
        {
            var lstconcerts = await _concertRepo.GetAllAsych();
            return Json(new { data = lstconcerts ?? null });
        }
        public async Task<IActionResult> Index()
        {
            return View(await _concertRepo.GetAllAsych());
        }

        public async Task<IActionResult> CreateUpdate(int id)
        {
            if (id != 0)
            {
                var concert = await _concertRepo.GetByIdAsych(id);
                if (concert == null || concert.Id == 0) return NotFound();

                //createConcertVM = new CreateConcertVM
                //{
                //    Id = concert.Id,
                //    Name = concert.Name,
                //    Description = concert.Description,
                //    DateTime = concert.DateTime,
                //    ArtistId = concert.ArtistId,
                //    VenueId = concert.VenueId,
                //    strImageUrl = concert.ImageUrl,
                //};
                createConcertVM = _mapper.Map<CreateConcertVM>(concert);
            }

            var artists = await _artistRepo.GetAllAsych();
            ViewBag.ArtistList = new SelectList(artists, "Id", "Name");

            var venues = await _venueRepo.GetAllAsych();
            ViewBag.VenuesList = new SelectList(venues, "Id", "Name");

            return View(createConcertVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUpdate(CreateConcertVM createConcertVM)
        {
            if (ModelState.IsValid)
            {
                if (createConcertVM != null && createConcertVM.Id > 0)
                {
                    var concertById = await _concertRepo.GetByIdAsych(createConcertVM.Id);
                    if (concertById == null) return NotFound();

                    //var concert = BindToView(createConcertVM);
                    var concert = _mapper.Map<Concert>(createConcertVM);
                    if (createConcertVM.ImageUrl != null)
                    {
                        concert.ImageUrl = await _utilityRepo.EditImage(ContainerName, createConcertVM.ImageUrl, concertById.ImageUrl);
                    }
                    else
                    {
                        concert.ImageUrl = concertById.ImageUrl;
                    }

                    await _concertRepo.Update(concert);
                    TempData["Success"] = "Concert Updated done !";
                }
                else
                {
                    //var concert = BindToView(createConcertVM);
                    var concert = _mapper.Map<Concert>(createConcertVM);
                    if (createConcertVM.ImageUrl != null)
                    {
                        concert.ImageUrl = await _utilityRepo.SaveImage(ContainerName, createConcertVM.ImageUrl);
                    }

                    await _concertRepo.Add(concert);
                    TempData["Success"] = "Concert inserted done !";
                }
            }
            else
            {
                TempData["Error"] = "Something went wrong !";
                return View(concertVM);
            }

            return RedirectToAction(nameof(Index));
        }

        #region DeleteAPICall
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var concertById = await _concertRepo.GetByIdAsych(id);
            if (concertById == null) return Json(new { success = false, error = "Error in fetching data" });
            else
            {
                await _concertRepo.Delete(concertById);

                return Json(new { success = true, message = "Concert deleted done." });
            }
        }
        #endregion
        public async Task<IActionResult> GetTickets(int concertId)
        {
            var bookings = await _bookingRepo.GetAllBookingAsync(concertId);
            //var vm = bookings.Select(b=> new DashboardVM
            //{
            //    UserName = b.User.UserName,
            //    ConcertName = b.Concert.Name,
            //    SeatNumber = string.Join(",",b.Tickets.Select(b=>b.SeatNumber))
            //}).ToList();
            var vm = bookings.Select(b => _mapper.Map<DashboardVM>(b)).ToList();
            return View(vm);
        }
        private Concert BindToView(CreateConcertVM createConcertVM)
        {
            var model = new Concert
            {
                Id = createConcertVM.Id,
                Name = createConcertVM.Name,
                Description = createConcertVM.Description,
                DateTime = createConcertVM.DateTime,
                ArtistId = createConcertVM.ArtistId,
                VenueId = createConcertVM.VenueId,
            };
            return model;
        }
    }
}