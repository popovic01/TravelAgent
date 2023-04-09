using AutoMapper;
using TravelAgent.DTO.OfferRequest;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class OfferRequestProfile : Profile
    {
        public OfferRequestProfile()
        {
            CreateMap<OfferRequest, OfferRequestDTO>()
                .ForMember(dest => dest.TransportationTypeId, opt => opt.MapFrom(src => src.TransportationType.Id))
                .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id).ToList()))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id))
                .ReverseMap();
        }
    }
}
