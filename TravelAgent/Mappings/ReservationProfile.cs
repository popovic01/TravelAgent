using AutoMapper;
using TravelAgent.DTO.Reservation;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDTO>()
                .ReverseMap();

            CreateMap<Reservation, ReservationResponseDTO>()
                .ForMember(dest => dest.OfferName, opt => opt.MapFrom(src => src.Offer.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Offer.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Offer.EndDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Offer.Price))
                .ReverseMap();
        }
    }
}
