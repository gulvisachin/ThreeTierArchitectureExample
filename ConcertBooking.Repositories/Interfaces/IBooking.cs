using ConcertBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IBooking
    {
        Task AddBooking(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookingAsync(int concertId);
    }
}
