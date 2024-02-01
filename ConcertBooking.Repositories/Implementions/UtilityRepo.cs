using ConcertBooking.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConcertBooking.Repositories.Implementions
{
    public class UtilityRepo : IUtility
    {
        private IWebHostEnvironment _env;
        private IHttpContextAccessor _contextAccessor;
        public UtilityRepo(IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _env = env;
            _contextAccessor = contextAccessor;
        }

        public Task DeleteImage(string ContainerName, string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath)) { return Task.CompletedTask; }
            var filename = Path.GetFileName(dbPath);
            var completePath = Path.Combine(_env.WebRootPath,ContainerName, filename);
            if(File.Exists(completePath)) { File.Delete(completePath); }
            return Task.CompletedTask;
        }

        public async Task<string> EditImage(string ContainerName, IFormFile file, string dbPath)
        {
            await DeleteImage(ContainerName, dbPath);
            return await SaveImage(ContainerName, file);
        }

        //Guid : ascy-gu98-sc34
        //https://localhost:3400/ContainerName/ascy-gu98-sc34.jpg
        public async Task<string> SaveImage(string ContainerName, IFormFile file)
        {
            var extention = Path.GetExtension(file.FileName).ToLowerInvariant();
            var filename = $"{Guid.NewGuid()}{extention}";
            string folder = Path.Combine(_env.WebRootPath, ContainerName);
            if(!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }
            string folderPath = Path.Combine(folder, filename);

            #region Save Image to path
            using (var memorySteam = new MemoryStream())
            {
                await file.CopyToAsync(memorySteam);
                var content = memorySteam.ToArray();
                await File.WriteAllBytesAsync(folderPath, content);
            }
            #endregion

            var basePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var completePath = Path.Combine(basePath,ContainerName,filename).Replace('\\', '/');
            return completePath;

        }
    }
}
