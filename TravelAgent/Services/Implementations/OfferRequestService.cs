using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ResponsePackageNoData DeleteRequestedOffer(int id)
        {
            var retVal = new ResponsePackageNoData();

            var offerReq = _context.OfferRequests.FirstOrDefault(x => x.Id == id);

            if (offerReq == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji zahtev za ponudom sa id-jem {id}";
            }
            else
            {
                _context.OfferRequests.Remove(offerReq);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisan zahtev za ponudom";
            }
            return retVal;
        }

        public PaginationDataOut<OfferRequestDTO> GetAllRequestedOffers(RequestDTO pageInfo)
        {
            PaginationDataOut<OfferRequestDTO> retVal = new ();

            IQueryable<OfferRequest> offerRequests = _context.OfferRequests
                .Include(x => x.TransportationType)
                .Include(x => x.Locations);

            if (pageInfo.ClientId.HasValue)
            {
                offerRequests = offerRequests.Where(x => x.Client.Id == pageInfo.ClientId);
            }

            retVal.Count = offerRequests.Count();

            offerRequests = offerRequests
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            offerRequests.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferRequestDTO>(x)));

            foreach (var offerReq in retVal.Data)
            {
                offerReq.OfferId = _context.Offers.FirstOrDefault(x => x.OfferRequestId == offerReq.Id)?.Id;
            }

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
                retVal.Message = $"Ne postoji zahtev za ponudom sa id-jem {offerReqId}";
            }
            else
            {
                retVal.TransferObject = _mapper.Map<OfferRequestDTO>(offerReq);
            }

            return retVal;
        }

        public ResponsePackageNoData RequestOffer(OfferRequestDTO dataIn)
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
                    TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Name == dataIn.TransportationType),
                    Locations = _context.Locations.Where(x => dataIn.Locations.Contains(x.Name)).ToList()
                };

                _context.OfferRequests.Add(offerReqDb);
                _context.SaveChanges();
                retVal.Message = $"Zahtev za ponudom je uspešno poslat";
            }
            catch (Exception ex)
            {
                retVal.Message = "Došlo je do greške";
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
                retVal.Message = $"Ne postoji zahtev za ponudom sa id-jem {id}";
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
                offerReqDb.TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Name == dataIn.TransportationType);
                offerReqDb.Locations = _context.Locations.Where(x => dataIn.Locations.Contains(x.Name)).ToList();
                _context.SaveChanges();

                retVal.Message = $"Uspešno izmenjen zahtev za ponudom";
            }
            catch (Exception ex)
            {
                retVal.Message = "Došlo je do greške";
                retVal.Status = 400;
            }

            return retVal;
        }
    }
}
