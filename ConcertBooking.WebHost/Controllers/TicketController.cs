using AutoMapper;
using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using ConcertBooking.WebHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConcertBooking.WebHost.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicket _ticketRepo;
        private readonly IMapper _mapper;
        public TicketController(ITicket ticketRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var bookings = await _ticketRepo.GetBooking(userId);
            //List<BookingVM> bookingsVM = new List<BookingVM>();
            //foreach (var booking in bookings)
            //{
            //    bookingsVM.Add(new BookingVM
            //    {
            //        BookingId = booking.Id,
            //        BookingDate = booking.BookingDate,
            //        ConcertName = booking?.Concert?.Name ?? "",
            //        Tickets = booking.Tickets.Select(ticket => new TicketVM
            //        {
            //            SeatNumber = ticket.SeatNumber
            //        }).ToList(),
            //    }); 
            //}
            var bookingsVM = bookings.Select(booking => _mapper.Map<BookingVM>(booking)).ToList();
            return View(bookingsVM);
        }
    }
}
