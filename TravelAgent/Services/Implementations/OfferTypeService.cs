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
                retVal.Message = $"Već postoji tip ponude {offerType.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.OfferTypes.Add(_mapper.Map<OfferType>(offerType));
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodat tip ponude {offerType.Name}";
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
                retVal.Message = $"Ne postoji tip ponude sa id-jem {id}";
            }
            else
            {
                _context.OfferTypes.Remove(offerType);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisan tip ponude {offerType.Name}";
            }
            return retVal;
        }

        public PaginationDataOut<OfferTypeDTO> GetAll(PageInfo pageInfo)
        {
            PaginationDataOut<OfferTypeDTO> retVal = new PaginationDataOut<OfferTypeDTO>();

            IQueryable<OfferType> offerTypes = _context.OfferTypes;
            retVal.Count = offerTypes.Count();

            offerTypes = offerTypes
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page))
                .Take(pageInfo.PageSize);

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
                retVal.Message = $"Ne postoji tip ponude sa id-jem {id}";
            }
            else if (offerNameDb != null)
            {
                retVal.Message = $"Već postoji tip ponude {offerType.Name}";
                retVal.Status = 409;
            }
            else
            {
                offerTypeDb.Name = offerType.Name;
                _context.SaveChanges();
                retVal.Message = $"Uspešno izmenjen tip ponude {offerType.Name}";
            }

            return retVal;
        }
    }
}
