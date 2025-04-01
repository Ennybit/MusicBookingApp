using AutoMapper;
using MusicBookingApp.Model;
using MusicBookingApp.DTOs;

namespace MusicBookingApp.MapperInitializer
{
    public class Initializer : Profile
    {
        public Initializer()
        {
            CreateMap<Users, RegistrationDTO>().ReverseMap();
            CreateMap<Users, LoginDTO>().ReverseMap();
            CreateMap<Artists, CreateArtistDTO>().ReverseMap();
            CreateMap<Artists, UpdateArtistDTO>().ReverseMap();
            CreateMap<Artists, GetArtistDTO>().ReverseMap();
            CreateMap<Events, CreateEventDTO>().ReverseMap();
            CreateMap<Events, UpdateEventDTO>().ReverseMap();
            CreateMap<Events, GetEventDTO>().ReverseMap();
            CreateMap<Bookings, CreateBookingsDTO>().ReverseMap();
            CreateMap<Bookings, GetBookingsDTO>().ReverseMap();
            CreateMap<Bookings, GetBookingsByIDDTO>().ReverseMap();

        }
    }
}
