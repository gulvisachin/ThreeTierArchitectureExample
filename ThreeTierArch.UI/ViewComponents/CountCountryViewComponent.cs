using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Implementions;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.UI.ViewComponents
{
    [ViewComponent(Name = "CountCountry")]
    public class CountCountryViewComponent : ViewComponent
    {
        private ICountry _countryRepo;

        public CountCountryViewComponent(ICountry countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var countries = await _countryRepo.GetAllAsych();
            return View(countries.Count());

        }
    }
}
