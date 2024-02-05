using AutoMapper;
using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.Extensions;
using ConcertBooking.WebHost.Models;
using ConcertBooking.WebHost.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Claims;

namespace ConcertBooking.WebHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConcert _concertRepo;
        private readonly ITicket _ticketRepo;
        private readonly IBooking _bookingRepo;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IConcert concertRepo, ITicket ticketRepo, IBooking bookingRepo, IMapper mapper)
        {
            _logger = logger;
            _concertRepo = concertRepo;
            _ticketRepo = ticketRepo;
            _bookingRepo = bookingRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var todayDate = DateTime.Now;
            var concerts = await _concertRepo.GetAllAsych();
            //var activeConcerts = concerts.Where(x => x.DateTime.Date >= todayDate).Select(x => new HomeViewModel
            //{
            //    ConcertId = x.Id,
            //    ConcertName = x.Name,
            //    ConcertDescription = x.Description.Length > 100 ? x.Description.Substring(0, 100) : x.Description,
            //    ConcertAddress = x.Venue.Name,
            //    ArtistName = x.Artist.Name,
            //    ConcertImage = x.ImageUrl,
            //}).ToList();
            var activeConcerts = concerts.Where(x => x.DateTime.Date >= todayDate).Select(c=> _mapper.Map<HomeViewModel>(c)).ToList();
            return View(activeConcerts);
        }

        public IActionResult CountSummary()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var concert = await _concertRepo.GetByIdAsych(id);
            if(concert == null) return NotFound();
            //var vm = new HomeViewModel
            //{
            //    ConcertId = concert.Id,
            //    ConcertName = concert.Name,
            //    ConcertDescription = concert.Description.Length > 100 ? concert.Description.Substring(0, 100) : concert.Description,
            //    ConcertAddress = concert.Venue.Address,
            //    ConcertImage = concert.ImageUrl,
            //    ConcertDateTime = concert.DateTime,
            //    ArtistName = concert.Artist.Name,
            //    ArtistImage = concert.Artist.ImageUrl,
            //    VenueName = concert.Venue.Name,
            //    VenueAddress = concert.Venue.Address
            //};
            #region Implement Auto mapper feature
            var vm = _mapper.Map<HomeViewModel>(concert);
            #endregion

            return View(vm);
        }
        [Authorize]
        public async Task<IActionResult> AvailableTickets(int concertId)
        {
            var concert = await _concertRepo.GetByIdAsych(concertId);
            if(concert == null) return NotFound();
            var allSeats = Enumerable.Range(1, concert.Venue.SeatCapacity).ToList();//1,2,3,4,5
            var bookedTickets = await _ticketRepo.GetBookedTickets(concert.Id);//2,3
            var availableSeats = allSeats.Except(bookedTickets).ToList();//1,4,5

            //var vm = new AvailableTicketVM
            //{
            //    ConcertId = concert.Id,
            //    ConcertName = concert.Name,
            //    AvailableSeats = availableSeats
            //};
            var vm = _mapper.Map<AvailableTicketVM>(concert);
            vm.AvailableSeats = availableSeats;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> BookTickets(int ConcertId, List<int> selectedSeats)
        {
            //if(selectedSeats == null || selectedSeats.Count == 0)
            //{
            //    ModelState.AddModelError("", "No seat selected");
            //    return RedirectToAction(nameof(AvailableTickets), new { concertId = ConcertId });
            //}


            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = claim.Value;

            //var newBooking = new Booking
            //{
            //    ConcertId = ConcertId,
            //    BookingDate = DateTime.Now,
            //    UserId = userId
            //};

            //foreach (var seat in selectedSeats)
            //{
            //    newBooking.Tickets.Add(new Ticket
            //    {
            //        SeatNumber = seat,
            //        IsBooked = true,
            //    });
            //}
            if (selectedSeats.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "No seat selected");
                return RedirectToAction(nameof(AvailableTickets), new { concertId = ConcertId });
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var newBooking = new Booking
            {
                ConcertId = ConcertId,
                BookingDate = DateTime.Now,
                UserId = userId,
                Tickets = selectedSeats.Select(seat =>new Ticket{
                    SeatNumber = seat,
                    IsBooked = true,
                }).ToList(),
            };
            await _bookingRepo.AddBooking(newBooking);
            TempData["Success"] = "Tickets booked successfully !";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}