using ConcertBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IVenue
    {
        Task<IEnumerable<Venue>> GetAllAsych();
        Task<Venue> GetByIdAsych(int id);
        Task Update(Venue venue);
        Task Delete(Venue venue);
        Task Add(Venue venue);
    }
}
