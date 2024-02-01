using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ThreeTierArch.Entities;
namespace ThreeTierArch.UI.ViewModels
{
    public class StateVM
    {
        public State state { get; set; } = new State();
        //public int CountryId { get; set; }
        [ValidateNever]
        public IEnumerable<State> states { get; set; } = new List<State>();
        [ValidateNever]
        public IEnumerable<Country> countries { get; set; } = new List<Country>();
    }
}
