using AutoMapper;
using TravelAgent.DTO.TransportationType;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class TransportationTypeProfile : Profile
    {
        public TransportationTypeProfile()
        {
            CreateMap<TransportationType, TransportationTypeDTO>().ReverseMap();
        }
    }
}
