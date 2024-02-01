using ThreeTierArch.Entities;

namespace ThreeTierArch.UI.ViewModels
{
    public class CityVM
    {
        public City City { get; set; } = new City();
        public IEnumerable<City> cities { get; set; } = new List<City>();
    }
}
