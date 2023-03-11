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
                .ForMember(dest => dest.OfferTypeId, opt => opt.MapFrom(src => src.OfferType.Id))
                .ForMember(dest => dest.TransportationTypeId, opt => opt.MapFrom(src => src.TransportationType.Id))
                .ForMember(dest => dest.LocationsIds, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ReverseMap();
            CreateMap<Offer, OfferDTO>()
                .ForMember(dest => dest.LocationsIds, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ReverseMap();
        }
    }
}
