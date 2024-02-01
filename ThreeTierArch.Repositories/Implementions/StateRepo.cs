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
    public class StateRepo : IState
    {
        private readonly ApplicationDBContext _context;

        public StateRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Add(State state)
        {
            await _context.States.AddAsync(state);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(State state)
        {
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<State>> GetAllAsych()
        {
            return await _context.States.Include(x => x.Country).ToListAsync();
            //return await _context.States.ToListAsync();
        }

        public async Task<State> GetByIdAsych(int id)
        {
            return await _context.States.FindAsync(id);
        }

        public async Task Update(State State)
        {
            _context.States.Update(State);
            await _context.SaveChangesAsync();
        }
    }
}
