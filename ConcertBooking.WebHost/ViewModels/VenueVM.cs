using ConcertBooking.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ConcertBooking.WebHost.ViewModels
{
    public class VenueVM
    {
        public Venue venue { get; set; } = new Venue();
        [ValidateNever]
        public IEnumerable<Venue> venues { get; set; } = new List<Venue>();
    }
}
