using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferRequest;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class OfferRequestService : IOfferRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfferRequestService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData DeleteRequestedOffer(int clientId, int offerReqId)
        {
            throw new NotImplementedException();
        }

        public ResponsePackage<List<OfferRequestDTO>> GetAllRequestedOffers()
        {
            throw new NotImplementedException();
        }

        public ResponsePackage<OfferRequestDTO> GetRequestedOfferById(int offerReqId)
        {
            throw new NotImplementedException();
        }

        public ResponsePackageNoData RequestOffer(int clientId, OfferRequestDTO dataIn)
        {
            var retVal = new ResponsePackageNoData();

            try
            {
                var offerReqDb = new OfferRequest()
                {
                    MaxPrice = dataIn.MaxPrice,
                    DepartureLocation = dataIn.DepartureLocation,
                    StartDate = dataIn.StartDate,
                    EndDate = dataIn.EndDate,
                    SpotNumber = dataIn.SpotNumber,
                    TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == dataIn.TransportationTypeId),
                    Locations = _context.Locations.Where(x => dataIn.LocationIds.Contains(x.Id)).ToList()
                };

                _context.OfferRequests.Add(offerReqDb);
                _context.SaveChanges();
                retVal.Message = $"Your Offer Reqest has been successfully submitted.";
            }
            catch (Exception ex)
            {
                retVal.Message = "Something went wrong";
                retVal.Status = 400;
            }
            return retVal;
        }

        public ResponsePackageNoData UpdateRequestedOffer(int clientId, OfferRequestDTO dataIn)
        {
            throw new NotImplementedException();
        }
    }
}
