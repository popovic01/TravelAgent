using AutoMapper;
using TravelAgent.DTO.Reservation;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
        }
    }
}
