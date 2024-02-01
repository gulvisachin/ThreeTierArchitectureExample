namespace ConcertBooking.WebHost.ViewModels.HomeViewModels
{
    public class HomeViewModel
    {
        public int ConcertId { get; set; }
        public string ConcertName { get; set; } = string.Empty;
        public string ConcertDescription { get; set;} = string.Empty;
        public string ConcertAddress { get; set; } = string.Empty;
        public DateTime ConcertDateTime { get; set; } 
        public string ArtistName { get; set; } = string.Empty;
        public string ArtistImage { get; set; } = string.Empty;
        public string VenueName { get; set; } = string.Empty;
        public string VenueAddress { get; set; } = string.Empty;
        public string ConcertImage { get; set; } = string.Empty;
    }
}
