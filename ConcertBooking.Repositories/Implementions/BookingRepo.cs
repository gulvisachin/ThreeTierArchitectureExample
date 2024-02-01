using ConcertBooking.Entities;
using ConcertBooking.Repositories.DataAccess;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Implementions
{
    public class BookingRepo : IBooking
    {
        private readonly ApplicationDBContext _context;

        public BookingRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingAsync(int concertId)
        {
            return await _context.Bookings.Include(x=>x.User).Include(y=>y.Concert).Include(m=>m.Tickets).Where(z=>z.ConcertId.Equals(concertId)).ToListAsync();
        }
    }
}
