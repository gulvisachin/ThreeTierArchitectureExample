using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.DataAccess;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementions
{
    public class CityRepo : ICity
    {
        private readonly ApplicationDBContext _context;

        public CityRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Add(City city)
        {
            await _context.Cities.AddAsync(city);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(City city)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            var result = await _context.Cities.Include(x => x.State).ThenInclude(y => y.Country).ToListAsync();
            return result;
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task Update(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }
    }
}
