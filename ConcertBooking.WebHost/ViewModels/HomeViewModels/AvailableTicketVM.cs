namespace ConcertBooking.WebHost.ViewModels.HomeViewModels
{
    public class AvailableTicketVM
    {
        public int ConcertId { get; set; }
        public string ConcertName { get; set; }   
        public List<int> AvailableSeats { get; set; }
    }
}
