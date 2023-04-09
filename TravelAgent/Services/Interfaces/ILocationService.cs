using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;

namespace TravelAgent.Services.Interfaces
{
    public interface ILocationService
    {
        public PaginationDataOut<LocationDTO> GetAll(FilterParamsDTO filterParams);
        public ResponsePackage<LocationDTO> Get(int id);
        public ResponsePackageNoData Add(LocationDTO location);
        public ResponsePackageNoData Update(int id, LocationDTO location);
        public ResponsePackageNoData Delete(int id);
    }
}
