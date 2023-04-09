using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferRequest;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferRequestService
    {
        public ResponsePackageNoData RequestOffer(int clientId, OfferRequestDTO dataIn);
        public ResponsePackageNoData UpdateRequestedOffer(int clientId, OfferRequestDTO dataIn);
        public ResponsePackageNoData DeleteRequestedOffer(int clientId, int offerReqId);
        public PaginationDataOut<OfferRequestDTO> GetAllRequestedOffers(PageInfo pageInfo);
        public ResponsePackage<OfferRequestDTO> GetRequestedOfferById(int offerReqId);
    }
}
