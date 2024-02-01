using ConcertBooking.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConcertBooking.WebHost.ViewCompenents
{
    [ViewComponent(Name = "CountBooking")]
    public class CountBookingViewComponent : ViewComponent
    {
        private readonly ITicket _ticketRepo;

        public CountBookingViewComponent(ITicket ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var bookings = await _ticketRepo.GetBooking(userId);
            return View(bookings.Count());
        }
    }
}
