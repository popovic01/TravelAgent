using AutoMapper;
using TravelAgent.DTO.OfferType;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class OfferTypeProfile : Profile
    {
        public OfferTypeProfile()
        {
            CreateMap<OfferType, OfferTypeDTO>().ReverseMap();
        }
    }
}
