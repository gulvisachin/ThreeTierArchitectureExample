using ConcertBooking.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ConcertBooking.WebHost.ViewModels
{
    public class ConcertVM
    {
        public Concert concert { get; set; } = new Concert();
        [ValidateNever]
        public IEnumerable<Concert> concerts { get; set; } = new List<Concert>();
    }
}
