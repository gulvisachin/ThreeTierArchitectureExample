using AutoMapper;
using ConcertBooking.Entities;
using ConcertBooking.WebHost.ViewModels;
using ConcertBooking.WebHost.ViewModels.HomeViewModels;

namespace ConcertBooking.WebHost.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Artist, CreateArtistVM>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.strImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ReverseMap();

            CreateMap<Concert, CreateConcertVM>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.strImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ReverseMap();

            CreateMap<Concert, AvailableTicketVM>()
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ConcertName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AvailableSeats, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Concert,HomeViewModel>()
                .ForMember(dest=>dest.ConcertId,opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.ConcertName,opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest=>dest.ConcertDescription,opt=>opt.MapFrom(src=>src.Description.Length > 100 ? src.Description.Substring(0, 100) : src.Description))
                .ForMember(dest => dest.ConcertAddress, opt => opt.MapFrom(src => src.Venue.Address))
                .ForMember(dest => dest.ConcertImage, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ConcertDateTime, opt => opt.MapFrom(src => src.DateTime))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.ArtistImage, opt => opt.MapFrom(src => src.Artist.ImageUrl))
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
                .ForMember(dest => dest.VenueAddress, opt => opt.MapFrom(src => src.Venue.Address))
                .ReverseMap();

            CreateMap<Booking, DashboardVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ConcertName, opt => opt.MapFrom(src => src.Concert.Name))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => string.Join(",", src.Tickets.Select(b => b.SeatNumber))));

            CreateMap<Booking,BookingVM>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest=> dest.ConcertName, opt=>opt.MapFrom(src => src.Concert.Name))
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets.Select(ticket => new TicketVM { SeatNumber = ticket.SeatNumber})))
                .ReverseMap();


        }
    }
}
