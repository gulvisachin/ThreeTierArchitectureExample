using ConcertBooking.Entities;
using ConcertBooking.Repositories.DataAccess;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.Repositories.Implementions
{
    public class ConcertRepo : IConcert
    {
        private readonly ApplicationDBContext _context;

        public ConcertRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Add(Concert concert)
        {
            await _context.Concerts.AddAsync(concert);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Concert concert)
        {
            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Concert>> GetAllAsych()
        {
            return await _context.Concerts.Include(x=>x.Artist).Include(y=>y.Venue).ToListAsync();
        }

        public async Task<Concert> GetByIdAsych(int id)
        {
            return await _context.Concerts.Include(x => x.Artist).Include(y => y.Venue).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task Update(Concert concert)
        {
            _context.Concerts.Update(concert);
            await _context.SaveChangesAsync();
        }
    }
}
