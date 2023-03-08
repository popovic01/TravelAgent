using TravelAgent.DTO.Common;
using TravelAgent.DTO.User;

namespace TravelAgent.Services.Interfaces
{
    public interface IUserService
    {
        public PaginationDataOut<UserResponseDTO> GetAll(SearchDTO searchData);
        public ResponsePackage<UserResponseDTO> Get(int id);
        public ResponsePackageNoData Register(UserRequestDTO user);
        public ResponsePackageNoData Login(UserRequestDTO user);
        public ResponsePackageNoData Logout(UserRequestDTO user);
        public ResponsePackageNoData Update(int id, UserRequestDTO user);
        public ResponsePackageNoData Delete(int id);
    }
}
