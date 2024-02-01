using ConcertBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IArtist
    {
        Task<IEnumerable<Artist>> GetAllAsych();
        Task<Artist> GetByIdAsych(int id);
        Task Update(Artist artist);
        Task Delete(Artist artist);
        Task Add(Artist artist);
    }
}
