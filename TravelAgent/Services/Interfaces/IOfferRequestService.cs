using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferRequest;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferRequestService
    {
        public ResponsePackageNoData RequestOffer(int clientId, OfferRequestDTO dataIn);
        public ResponsePackageNoData UpdateRequestedOffer(int id, OfferRequestDTO dataIn);
        public ResponsePackageNoData DeleteRequestedOffer(int id);
        public PaginationDataOut<OfferRequestDTO> GetAllRequestedOffers(PageInfo pageInfo);
        public PaginationDataOut<OfferRequestDTO> GetAllByUser(PageInfo pageInfo, int id);
        public ResponsePackage<OfferRequestDTO> GetRequestedOfferById(int offerReqId);
    }
}
