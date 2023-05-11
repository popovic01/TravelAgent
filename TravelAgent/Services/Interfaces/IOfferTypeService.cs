using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferType;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferTypeService
    {
        public PaginationDataOut<OfferTypeDTO> GetAll(PageInfo pageInfo);
        public ResponsePackageNoData Add(OfferTypeDTO offerType);
        public ResponsePackageNoData Update(int id, OfferTypeDTO offerType);
        public ResponsePackageNoData Delete(int id);
    }
}
