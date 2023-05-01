using AutoMapper;
using TravelAgent.DTO.Tag;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Tag, TagIdDTO>().ReverseMap();
        }
    }
}
