using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface ICountry
    {
        Task<IEnumerable<Country>> GetAllAsych();
        Task<Country> GetByIdAsych(int id);
        Task Update(Country country);
        Task Delete(Country country);
        Task Add(Country country);
    }
}
