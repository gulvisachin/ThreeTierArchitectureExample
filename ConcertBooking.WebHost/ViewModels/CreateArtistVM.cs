namespace ConcertBooking.WebHost.ViewModels
{
    public class CreateArtistVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? strImageUrl { get; set; }
    }
}
