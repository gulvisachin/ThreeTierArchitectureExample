using ConcertBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IConcert
    {
        Task<IEnumerable<Concert>> GetAllAsych();
        Task<Concert> GetByIdAsych(int id);
        Task Update(Concert concert);
        Task Delete(Concert concert);
        Task Add(Concert concert);
    }
}
