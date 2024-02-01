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
    public class CountryRepo : ICountry
    {
        private readonly ApplicationDBContext _context;

        public CountryRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Add(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Country>> GetAllAsych()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetByIdAsych(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task Update(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }
       
    }
}
