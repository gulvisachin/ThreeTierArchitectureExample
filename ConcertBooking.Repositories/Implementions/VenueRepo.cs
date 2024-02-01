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
    public class VenueRepo : IVenue
    {
        private readonly ApplicationDBContext _context;

        public VenueRepo(ApplicationDBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Venue venue)
        {
            await _context.Venues.AddAsync(venue);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Venue venue)
        {
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Venue>> GetAllAsych()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue> GetByIdAsych(int id)
        {
            return await _context.Venues.FindAsync(id);
        }

        public async Task Update(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }
    }
}
