using AutoMapper;
using TravelAgent.DTO.User;
using TravelAgent.Model;

namespace TravelAgent.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<User, UserRequestDTO>().ReverseMap();
        }
    }
}
