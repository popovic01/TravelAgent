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
                .ForMember(dest => dest.TransportationType, opt => opt.MapFrom(src => src.TransportationType.Name))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Name).ToList()))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id))
                .ReverseMap();
        }
    }
}
