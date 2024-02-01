using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeTierArch.Repositories.Interfaces
{
    public interface IUtility
    {
        Task<string> EditImage(string ContainerName, IFormFile file, string dbPath);
        Task<string> SaveImage(string ContainerName, IFormFile file);
        Task DeleteImage(string ContainerName, string dbPath);
    }
}
