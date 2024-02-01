using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IState
    {
        Task<IEnumerable<State>> GetAllAsych();
        Task<State> GetByIdAsych(int id);
        Task Update(State state);
        Task Delete(State state);
        Task Add(State state);
    }
}
