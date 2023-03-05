using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;
using TravelAgent.DTO.Offer;

namespace TravelAgent.Services.Interfaces
{
    public interface ILocationService
    {
        public PaginationDataOut<LocationDTO> GetAll(SearchDTO searchData);
        public ResponsePackage<LocationDTO> Get(int id);
        public ResponsePackageNoData Add(LocationDTO location);
        public ResponsePackageNoData Update(int id, LocationDTO location);
        public ResponsePackageNoData Delete(int id);
    }
}
