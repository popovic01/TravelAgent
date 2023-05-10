using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Offer;
using TravelAgent.Helpers;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonHelper _commonHelper;

        public OfferService(ApplicationDbContext context, IMapper mapper, ICommonHelper commonHelper)
        {
            _context = context;
            _mapper = mapper;
            _commonHelper = commonHelper;
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
                    Rating = offer.Rating,
                    AvailableSpots = offer.AvailableSpots,
                    OfferCode = _commonHelper.RandomString(6),
                    OfferType = _context.OfferTypes.FirstOrDefault(x => x.Name == offer.OfferType),
                    TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Name == offer.TransportationType),
                    Locations = _context.Locations.Where(x => offer.Locations.Contains(x.Name)).ToList(),
                    Tags = _context.Tags.Where(x => offer.Tags.Contains(x.Name)).ToList()
                };

                _context.Offers.Add(offerDb);
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodata ponuda {offer.Name}";
            }
            catch (Exception ex)
            {
                retVal.Message = "Došlo je do greške";
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
                retVal.Message = $"Ne postoji ponuda sa id-jem {id}";
            }
            else
            {
                _context.Offers.Remove(offer);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisana ponuda sa id-jem {id}";
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
                retVal.Message = $"Ne postoji ponuda sa id-jem {offerId}";
            }
            else if (location == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji lookacija sa id-jem {locationId}";
            }
            else
            {
                offer.Locations.Remove(location);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisana lokacija {locationId} za ponudu sa id-jem {offerId}";
            }
            return retVal;
        }

        public ResponsePackageNoData DeleteTagForOffer(int offerId, int tagId)
        {
            var retVal = new ResponsePackageNoData();

            var offer = _context.Offers
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == offerId);

            var tag = _context.Tags
                .FirstOrDefault(x => x.Id == tagId);

            if (offer == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji ponuda sa id-jem {offerId}";
            }
            else if (tag == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji tag sa id-jem {tagId}";
            }
            else
            {
                offer.Tags.Remove(tag);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisan tag {tagId} za ponudu sa id-jem {offerId}";
            }
            return retVal;
        }

        public ResponsePackageNoData AddOfferToWishlist(int offerId, int clientId)
        {
            var retVal = new ResponsePackageNoData();

            var offer = _context.Offers
                .Include(x => x.Clients)
                .FirstOrDefault(x => x.Id == offerId);

            var client = (Client)_context.Users
                .FirstOrDefault(x => x.Id == clientId);

            if (offer != null && client != null)
            {
                offer.Clients.Add(client);
                _commonHelper.ExecuteProcedure("WISHLIST_PROCEDURE", offerId, 1);
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodata ponuda {offer.Name} u listu želja";
            }
            else
            {
                retVal.Status = 404;
                retVal.Message = "Došlo je do greške";
            }

            return retVal;
        }

        public ResponsePackageNoData RemoveOfferFromWishlist(int offerId, int clientId)
        {
            var retVal = new ResponsePackageNoData();

            var offer = _context.Offers
                .Include(x => x.Clients)
                .FirstOrDefault(x => x.Id == offerId);

            var client = (Client)_context.Users
                .FirstOrDefault(x => x.Id == clientId);

            if (offer != null && client != null)
            {
                offer.Clients.Remove(client);
                _commonHelper.ExecuteProcedure("WISHLIST_PROCEDURE", offerId, 0);
                _context.SaveChanges();
                retVal.Message = $"Uspešno je uklonjena ponuda {offer.Name} iz liste želja";
            }
            else
            {
                retVal.Status = 404;
                retVal.Message = "Došlo je do greške";
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
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == id);

            if (offer == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji ponuda sa id-jem {id}";
            }
            else
            {
                retVal.TransferObject = _mapper.Map<OfferReviewDTO>(offer);
                retVal.TransferObject.Duration = retVal.TransferObject.EndDate.Subtract(retVal.TransferObject.StartDate).Days;
            }

            return retVal;
        }

        public PaginationDataOut<OfferIdDTO> GetAll(OfferPageInfo searchData)
        {
            PaginationDataOut<OfferIdDTO> retVal = new();

            var pageInfo = searchData.PageInfo;
            var filterParams = searchData.FilterParams;
            var searchString = filterParams.SearchFilter.ToLower();

            IQueryable<Offer> offers = _context.Offers
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Include(x => x.Locations)
                .Include(x => x.Tags);

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
            if (searchData.FilterParams.TagIds.Count > 0)
            {
                offers = offers.Where(x => x.Tags.Any(y => searchData.FilterParams.TagIds.Contains(y.Id)));
            }
            retVal.Count = offers.Count();

            offers = offers
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page - 1))
                .Take(pageInfo.PageSize);

            offers.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferIdDTO>(x)));

            foreach (var offer in retVal.Data)
                offer.Duration = offer.EndDate.Subtract(offer.StartDate).Days;

            return retVal;
        }

        public PaginationDataOut<OfferDTO> GetWishlist(int id)
        {
            PaginationDataOut<OfferDTO> retVal = new();

            IQueryable<Offer> wishlist = _context.Offers
                .Include(x => x.Clients)
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Where(x => x.Clients.Any(x => x.Id == id));

            retVal.Count = wishlist.Count();

            wishlist.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferDTO>(x)));

            foreach (var offer in retVal.Data)
                offer.Duration = offer.EndDate.Subtract(offer.StartDate).Days;

            return retVal;
        }

        public ResponsePackageNoData Update(int id, OfferReviewDTO offer)
        {
            var retVal = new ResponsePackageNoData();

            var offerDb = _context.Offers
                .Include(x => x.TransportationType)
                .Include(x => x.OfferType)
                .Include(x => x.Locations)
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == id);

            if (offerDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"Ne postoji ponuda sa id-jem {id}";
                return retVal;
            }
            try
            {
                offerDb.StartDate = offer.StartDate;
                offerDb.EndDate = offer.EndDate;
                offerDb.DepartureLocation = offer.DepartureLocation;
                offerDb.Name = offer.Name;
                offerDb.Description = offer.Description;
                offerDb.Price = offer.Price;
                offerDb.AvailableSpots = offer.AvailableSpots;
                offerDb.TransportationType = _context.TransportationTypes.FirstOrDefault(x => x.Name == offer.TransportationType);
                offerDb.OfferType = _context.OfferTypes.FirstOrDefault(x => x.Name == offer.OfferType);
                offerDb.Locations = _context.Locations.Where(x => offer.Locations.Contains(x.Name)).ToList();
                offerDb.Tags = _context.Tags.Where(x => offer.Tags.Contains(x.Name)).ToList();
                _context.SaveChanges();

                retVal.Message = $"Uspešno izmenjena ponuda {offer.Name}";
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
