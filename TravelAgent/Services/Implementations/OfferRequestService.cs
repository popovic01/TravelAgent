using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;
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

        public PaginationDataOut<OfferRequestDTO> GetAllRequestedOffers(PageInfo pageInfo)
        {
            PaginationDataOut<OfferRequestDTO> retVal = new PaginationDataOut<OfferRequestDTO>();

            IQueryable<OfferRequest> offerRequests = _context.OfferRequests
                .Include(x => x.TransportationType)
                .Include(x => x.Locations);

            retVal.Count = offerRequests.Count();

            offerRequests = offerRequests
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            offerRequests.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferRequestDTO>(x)));

            return retVal;
        }

        public ResponsePackage<OfferRequestDTO> GetRequestedOfferById(int offerReqId)
        {
            var retVal = new ResponsePackage<OfferRequestDTO>();

            var offerReq = _context.OfferRequests
                .Include(x => x.TransportationType)
                .Include(x => x.Locations)
                .FirstOrDefault(x => x.Id == offerReqId);

            if (offerReq == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {offerReqId}";
            }
            else
            {
                retVal.TransferObject = _mapper.Map<OfferRequestDTO>(offerReq);
            }

            return retVal;
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
