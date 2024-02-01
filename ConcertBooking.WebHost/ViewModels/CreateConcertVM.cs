using ConcertBooking.Entities;

namespace ConcertBooking.WebHost.ViewModels
{
    public class CreateConcertVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }

        public int VenueId { get; set; }
        public int ArtistId { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? strImageUrl { get; set; }
    }
}
