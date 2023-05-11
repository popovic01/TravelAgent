using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;

namespace TravelAgent.Services.Interfaces
{
    public interface ILocationService
    {
        public PaginationDataOut<LocationIdDTO> GetAll(PageInfo pageInfo);
        public ResponsePackageNoData Add(LocationDTO location);
        public ResponsePackageNoData Update(int id, LocationDTO location);
        public ResponsePackageNoData Delete(int id);
    }
}
