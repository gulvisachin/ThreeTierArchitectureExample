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

    public class ArtistRepo : IArtist
    {
        private readonly ApplicationDBContext _context;

        public ArtistRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Add(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllAsych()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetByIdAsych(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task Update(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }
    }
}
