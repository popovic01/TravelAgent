using AutoMapper;
using TravelAgent.DTO.Offer;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferReviewDTO>()
                .ForMember(dest => dest.OfferType, opt => opt.MapFrom(src => src.OfferType.Name))
                .ForMember(dest => dest.TransportationType, opt => opt.MapFrom(src => src.TransportationType.Name))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Id).ToList()))
                .ForMember(dest => dest.AvailableSpotsLeft, opt => opt.MapFrom(src => src.AvailableSpots - src.ReservationCount))
                .ReverseMap();

            CreateMap<Offer, OfferDTO>()
                .ForMember(dest => dest.TransportationType, opt => opt.MapFrom(src => src.TransportationType.Name))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Name).ToList()))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name).ToList()))
                .ForMember(dest => dest.AvailableSpotsLeft, opt => opt.MapFrom(src => src.AvailableSpots - src.ReservationCount))
                .ReverseMap();
        }
    }
}
