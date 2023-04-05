using TravelAgent.DTO.Common;
using TravelAgent.DTO.User;

namespace TravelAgent.Services.Interfaces
{
    public interface IUserService
    {
        public PaginationDataOut<UserResponseDTO> GetAll(SearchDTO searchData);
        public ResponsePackage<UserResponseDTO> Get(int id);
        public ResponsePackage<string> Register(UserRequestDTO user);
        public ResponsePackage<string> Login(UserLoginDTO user);
        public ResponsePackageNoData Update(int id, UserRequestDTO user);
        public ResponsePackageNoData Delete(int id);
    }
}
