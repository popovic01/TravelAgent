using AutoMapper;
using TravelAgent.DTO.OfferRequest;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class OfferRequestProfile : Profile
    {
        public OfferRequestProfile()
        {
            CreateMap<OfferRequest, OfferRequestDTO>().ReverseMap();
        }
    }
}
