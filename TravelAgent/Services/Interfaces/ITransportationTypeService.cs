using TravelAgent.DTO.Common;
using TravelAgent.DTO.TransportationType;

namespace TravelAgent.Services.Interfaces
{
    public interface ITransportationTypeService
    {
        public PaginationDataOut<TransportationTypeDTO> GetAll(FilterParamsDTO filterParams);
        public ResponsePackage<TransportationTypeDTO> Get(int id);
        public ResponsePackageNoData Add(TransportationTypeDTO transportationType);
        public ResponsePackageNoData Update(int id, TransportationTypeDTO transportationType);
        public ResponsePackageNoData Delete(int id);
    }
}
