using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferRequest;
using TravelAgent.DTO.Reservation;
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

        public ResponsePackageNoData DeleteRequestedOffer(int id)
        {
            var retVal = new ResponsePackageNoData();

            var offerReq = _context.OfferRequests.FirstOrDefault(x => x.Id == id);

            if (offerReq == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {id}";
            }
            else
            {
                _context.OfferRequests.Remove(offerReq);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Requested Offer with ID {id}";
            }
            return retVal;
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

        public PaginationDataOut<OfferRequestDTO> GetAllByUser(PageInfo pageInfo, int id)
        {
            PaginationDataOut<OfferRequestDTO> retVal = new PaginationDataOut<OfferRequestDTO>();

            IQueryable<OfferRequest> offerRequests = _context.OfferRequests
                .Include(x => x.TransportationType)
                .Include(x => x.Client)
                .Where(x => x.Client.Id == id);

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
                    Client = (Client)_context.Users.FirstOrDefault(x => x.Id == dataIn.ClientId),
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

        public ResponsePackageNoData UpdateRequestedOffer(int id, OfferRequestDTO dataIn)
        {
            var retVal = new ResponsePackageNoData();

            var offerReqDb = _context.OfferRequests
                .Include(x => x.TransportationType)
                .Include(x => x.Locations)
                .FirstOrDefault(x => x.Id == id);

            if (offerReqDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer Request with ID {id}";
                return retVal;
            }
            try
            {
                offerReqDb.StartDate = dataIn.StartDate;
                offerReqDb.EndDate = dataIn.EndDate;
                offerReqDb.DepartureLocation = dataIn.DepartureLocation;
                offerReqDb.MaxPrice = dataIn.MaxPrice;
                offerReqDb.SpotNumber = dataIn.SpotNumber;
                offerReqDb.Client = (Client)_context.Users.FirstOrDefault(x => x.Id == dataIn.ClientId);
                offerReqDb.TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == dataIn.TransportationTypeId);
                offerReqDb.Locations = _context.Locations.Where(x => dataIn.LocationIds.Contains(x.Id)).ToList();
                _context.SaveChanges();

                retVal.Message = $"Successfully updated Offer Requeste with ID {id}";
            }
            catch (Exception ex)
            {
                retVal.Message = "Something went wrong";
                retVal.Status = 400;
            }

            return retVal;
        }
    }
}
