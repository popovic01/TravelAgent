using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferRequest;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferRequestService
    {
        public ResponsePackageNoData RequestOffer(OfferRequestDTO dataIn);
        public ResponsePackageNoData UpdateRequestedOffer(int id, OfferRequestDTO dataIn);
        public ResponsePackageNoData DeleteRequestedOffer(int id);
        public PaginationDataOut<OfferRequestDTO> GetAllRequestedOffers(RequestDTO pageInfo);
        public ResponsePackage<OfferRequestDTO> GetRequestedOfferById(int offerReqId);
    }
}
