using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ThreeTierArch.Entities;

namespace ThreeTierArch.UI.ViewModels
{
    public class CountryVM
    {
        public Country country { get; set; } = new Country();
        [ValidateNever]
        public IEnumerable<Country> countries { get; set; } = new List<Country>();
    }
}
