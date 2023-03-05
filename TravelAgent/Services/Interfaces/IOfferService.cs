using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;

namespace TravelAgent.Services.Interfaces
{
    public interface IOfferService
    {
        public PaginationDataOut<OfferDTO> GetAll(OfferPageInfo searchData);
        public ResponsePackage<OfferReviewDTO> Get(int id);
        public ResponsePackageNoData Add(OfferReviewDTO offer);
        public ResponsePackageNoData Update(int id, OfferReviewDTO offer);
        public ResponsePackageNoData Delete(int id);
    }
}
