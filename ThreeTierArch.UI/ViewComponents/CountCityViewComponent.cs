using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    [ViewComponent(Name = "CountCity")]
    public class CountCityViewComponent : ViewComponent
    {
        private ICity _cityRepo;

        public CountCityViewComponent(ICity cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cities = await _cityRepo.GetAllAsync();
            return View(cities.Count());
                 
        }
    }
}
