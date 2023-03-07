using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferType;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class OfferTypeService : IOfferTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfferTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(OfferTypeDTO offerType)
        {
            var retVal = new ResponsePackageNoData();

            var offerTypeDb = _context.OfferTypes.FirstOrDefault(x => x.Name == offerType.Name);

            if (offerTypeDb != null)
            {
                retVal.Message = $"Already exists Offer Type {offerType.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.OfferTypes.Add(_mapper.Map<OfferType>(offerType));
                _context.SaveChanges();
                retVal.Message = $"Successfully added Offer Type {offerType.Name}";
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var offerType = _context.OfferTypes.FirstOrDefault(x => x.Id == id);

            if (offerType == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer Type with ID {id}";
            }
            else
            {
                _context.OfferTypes.Remove(offerType);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Offer Type {offerType.Name}";
            }
            return retVal;
        }

        public ResponsePackage<OfferTypeDTO> Get(int id)
        {
            var retVal = new ResponsePackage<OfferTypeDTO>();

            var offerType = _context.OfferTypes
                .FirstOrDefault(x => x.Id == id);

            if (offerType == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer Type with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<OfferTypeDTO>(offerType);

            return retVal;
        }

        public PaginationDataOut<OfferTypeDTO> GetAll(SearchDTO searchData)
        {
            PaginationDataOut<OfferTypeDTO> retVal = new PaginationDataOut<OfferTypeDTO>();

            IQueryable<OfferType> offerTypes = _context.OfferTypes;

            if (!string.IsNullOrWhiteSpace(searchData.SearchFilter))
            {
                offerTypes = offerTypes.Where(x => x.Name.ToLower().Contains(searchData.SearchFilter));
            }
            retVal.Count = offerTypes.Count();

            offerTypes.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<OfferTypeDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, OfferTypeDTO offerType)
        {
            var retVal = new ResponsePackageNoData();

            var offerTypeDb = _context.OfferTypes
                .FirstOrDefault(x => x.Id == id);
            var offerNameDb = _context.OfferTypes
                .FirstOrDefault(x => x.Name.ToLower() == offerType.Name.ToLower());

            if (offerTypeDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Offer Type with ID {id}";
            }
            else if (offerNameDb != null)
            {
                retVal.Message = $"Already exists Offer Type {offerType.Name}";
                retVal.Status = 409;
            }
            else
            {
                offerTypeDb.Name = offerType.Name;
                _context.SaveChanges();
                retVal.Message = $"Successfully updated Offer Type {offerType.Name}";
            }

            return retVal;
        }
    }
}
