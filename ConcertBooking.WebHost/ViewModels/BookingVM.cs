using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.WebHost.ViewModels
{
    public class BookingVM
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string ConcertName { get; set; }
        public List<TicketVM> Tickets { get; set; } = new List<TicketVM>();

    }
}
