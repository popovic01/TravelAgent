using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferService
    {
        public PaginationDataOut<OfferIdDTO> GetAll(OfferPageInfo searchData);
        public PaginationDataOut<OfferDTO> GetWishlist(int id);
        public ResponsePackage<OfferReviewDTO> Get(int id);
        public ResponsePackageNoData Add(string offerJson, IFormFile? image);
        public ResponsePackageNoData Update(int id, OfferReviewDTO offer);
        public ResponsePackageNoData Delete(int id);
        public ResponsePackageNoData DeleteLocationForOffer(int offerId, int locationId);
        public ResponsePackageNoData DeleteTagForOffer(int offerId, int tagId);
        public ResponsePackageNoData AddOfferToWishlist(int offerId, int clientId);
        public ResponsePackageNoData RemoveOfferFromWishlist(int offerId, int clientId);
    }
}
