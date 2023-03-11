using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfferService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(OfferReviewDTO offer)
        {
            var retVal = new ResponsePackageNoData();

            try
            {
                var offerDb = new Offer()
                {
                    Name = offer.Name,
                    Description = offer.Description,
                    Price = offer.Price,
                    DepartureLocation = offer.DepartureLocation,
                    StartDate = offer.StartDate,
                    EndDate = offer.EndDate,
                    Duration = offer.Duration,
                    Rating = offer.Rating,
                    OfferCode = RandomString(6),
                    OfferType = _context.OfferTypes.FirstOrDefault(x => x.Id == offer.OfferTypeId),
                    TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == offer.TransportationTypeId),
                    Locations = _context.Locations.Where(x => offer.LocationIds.Contains(x.Id)).ToList()
                };

                _context.Offers.Add(offerDb);
                _context.SaveChanges();
                retVal.Message = $"Successfully added Offer {offer.Name}";
            }
            catch (Exception ex)
            {
                retVal.Message = "Something went wrong";
                retVal.Status = 400;
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var offer = _context.Offers.FirstOrDefault(x => x.Id == id);

            if (offer == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {id}";
            }
            else
            {
                _context.Offers.Remove(offer);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Offer with ID {id}";
            }
            return retVal;
        }

        public ResponsePackageNoData DeleteLocationForOffer(int offerId, int locationId)
        {
            var retVal = new ResponsePackageNoData();

            var offer = _context.Offers
                .Include(x => x.Locations)
                .FirstOrDefault(x => x.Id == offerId);

            var location = _context.Locations
                .FirstOrDefault(x => x.Id == locationId);

            if (offer == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {offerId}";
            }
            else if (location == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Location with ID {locationId}";
            }
            else
            {
                offer.Locations.Remove(location);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Location {locationId} for Offer with ID {offerId}";
            }
            return retVal;
        }

        public ResponsePackage<OfferReviewDTO> Get(int id)
        {
            var retVal = new ResponsePackage<OfferReviewDTO>();

            var offer = _context.Offers
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Include(x => x.Locations)
                .FirstOrDefault(x => x.Id == id);

            if (offer == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<OfferReviewDTO>(offer);

            return retVal;
        }

        public PaginationDataOut<OfferDTO> GetAll(OfferPageInfo searchData)
        {
            PaginationDataOut<OfferDTO> retVal = new PaginationDataOut<OfferDTO>();

            var pageInfo = searchData.PageInfo;
            var filterParams = searchData.FilterParams;
            var searchString = filterParams.SearchFilter.ToLower();

            IQueryable<Offer> offers = _context.Offers
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Include(x => x.Locations);
            offers = offers
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                offers = offers.Where(x => x.Name.ToLower().Contains(searchString));
            }
            if (!string.IsNullOrEmpty(searchData.FilterParams.StartDate) && !string.IsNullOrEmpty(filterParams.EndDate))
            {
                DateTime startDate = Convert.ToDateTime(filterParams.StartDate);
                DateTime endDate = Convert.ToDateTime(filterParams.EndDate);
                offers = offers.Where(x => x.StartDate.Date.CompareTo(startDate.Date) == 0 && x.EndDate.Date.CompareTo(endDate.Date) == 0);
            }
            if (searchData.FilterParams.LocationIds.Count > 0)
            {
                offers = offers.Where(x => x.Locations.Any(y => searchData.FilterParams.LocationIds.Contains(y.Id)));
            }
            retVal.Count = offers.Count();

            offers.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, OfferReviewDTO offer)
        {
            var retVal = new ResponsePackageNoData();

            var offerDb = _context.Offers
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Include(x => x.Locations)
                .FirstOrDefault(x => x.Id == id);

            if (offerDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer with ID {id}";
                return retVal;
            }
            try
            {
                offerDb.StartDate = offer.StartDate;
                offerDb.EndDate = offer.EndDate;
                offerDb.Duration = offer.Duration;
                offerDb.DepartureLocation = offer.DepartureLocation;
                offerDb.Name = offer.Name;
                offerDb.Description = offer.Description;
                offerDb.Price = offer.Price;
                offerDb.TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == offer.TransportationTypeId);
                offerDb.OfferType = _context.OfferTypes.FirstOrDefault(x => x.Id == offer.OfferTypeId);
                offerDb.Locations = _context.Locations.Where(x => offer.LocationIds.Contains(x.Id)).ToList();
                _context.SaveChanges();

                retVal.Message = $"Successfully updated Offer {offer.Name}";
            }
            catch (Exception ex)
            {
                retVal.Message = "Something went wrong";
                retVal.Status = 400;
            }

            return retVal;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

    }
}
