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
                .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(t => t.Id).ToList()))
                .ReverseMap();
            CreateMap<Offer, OfferDTO>()
                .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(t => t.Id).ToList()))
                .ReverseMap();
        }
    }
}
