using AutoMapper;
using TravelAgent.DTO.Location;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
        }
    }
}
