using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;
using TravelAgent.DTO.OfferRequest;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferService
    {
        public PaginationDataOut<OfferDTO> GetAll(OfferPageInfo searchData);
        public ResponsePackage<OfferReviewDTO> Get(int id);
        public ResponsePackageNoData Add(OfferReviewDTO offer);
        public ResponsePackageNoData Update(int id, OfferReviewDTO offer);
        public ResponsePackageNoData Delete(int id);
        public ResponsePackageNoData DeleteLocationForOffer(int offerId, int locationId);
        public ResponsePackageNoData DeleteTagForOffer(int offerId, int tagId);
        public ResponsePackageNoData AddOfferToWishlist(int offerId, int clientId);
        public ResponsePackageNoData RemoveOfferFromWishlist(int offerId, int clientId);
    }
}
