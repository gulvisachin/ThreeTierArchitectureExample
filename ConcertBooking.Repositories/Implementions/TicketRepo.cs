using ConcertBooking.Entities;
using ConcertBooking.Repositories.DataAccess;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.Repositories.Implementions
{
    public class TicketRepo : ITicket
    {
        private readonly ApplicationDBContext _context;

        public TicketRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> GetBookedTickets(int concertId)
        {
            var result = await _context.Tickets.Include(y=>y.Booking).Where(t=>t.Booking.Concert.Id== concertId && t.IsBooked).Select(t=>t.SeatNumber).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Booking>> GetBooking(string userId)
        {
            var getBookings = await _context.Bookings.Where(x=>x.UserId.Equals(userId)).Include(y=>y.Tickets).Include(z=>z.Concert).ToListAsync();
            return getBookings;
        }
    }
}
