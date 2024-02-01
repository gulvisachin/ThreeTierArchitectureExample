using ConcertBooking.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ConcertBooking.WebHost.ViewModels
{
    public class ArtistVM
    {
        public Artist artist { get; set; } = new Artist();
        [ValidateNever]
        public IEnumerable<Artist> artists { get; set; } = new List<Artist>();
    }
}
