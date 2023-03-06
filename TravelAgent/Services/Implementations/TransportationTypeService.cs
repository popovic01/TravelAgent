using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.TransportationType;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class TransportationTypeService : ITransportationTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransportationTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(TransportationTypeDTO transportationType)
        {
            var retVal = new ResponsePackageNoData();

            var transportationTypeDb = _context.TransportationTypes.FirstOrDefault(x => x.Name == transportationType.Name);

            if (transportationTypeDb != null)
            {
                retVal.Message = $"Already exists Transportation Type {transportationType.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.TransportationTypes.Add(_mapper.Map<TransportationType>(transportationType));
                _context.SaveChanges();
                retVal.Message = $"Successfully added Transportation Type {transportationType.Name}";
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var transportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == id);

            if (transportationType == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Transportation Type with ID {id}";
            }
            else
            {
                _context.TransportationTypes.Remove(transportationType);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Transportation Type {transportationType.Name}";
            }
            return retVal;
        }

        public ResponsePackage<TransportationTypeDTO> Get(int id)
        {
            var retVal = new ResponsePackage<TransportationTypeDTO>();

            var transportationType = _context.TransportationTypes
                .FirstOrDefault(x => x.Id == id);

            if (transportationType == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Transportation Type with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<TransportationTypeDTO>(transportationType);

            return retVal;
        }

        public PaginationDataOut<TransportationTypeDTO> GetAll(SearchDTO searchData)
        {
            PaginationDataOut<TransportationTypeDTO> retVal = new PaginationDataOut<TransportationTypeDTO>();

            IQueryable<TransportationType> transportationTypes = _context.TransportationTypes;

            if (!string.IsNullOrWhiteSpace(searchData.SearchFilter))
            {
                transportationTypes = transportationTypes.Where(x => x.Name.ToLower().Contains(searchData.SearchFilter));
            }
            retVal.Count = transportationTypes.Count();

            transportationTypes.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<TransportationTypeDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, TransportationTypeDTO transportationType)
        {
            var retVal = new ResponsePackageNoData();

            var transportationTypeDb = _context.TransportationTypes
                .FirstOrDefault(x => x.Id == id);
            var transportationTypeNameDb = _context.TransportationTypes
                .FirstOrDefault(x => x.Name.ToLower() == transportationType.Name.ToLower());

            if (transportationTypeDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Transportation Type with ID {id}";
            }
            else if (transportationTypeNameDb != null)
            {
                retVal.Message = $"Already exists Transportation Type {transportationType.Name}";
                retVal.Status = 409;
            }
            else
            {
                transportationTypeDb.Name = transportationType.Name;
                _context.SaveChanges();
                retVal.Message = $"Successfully updated Transportation Type {transportationType.Name}";
            }

            return retVal;
        }
    }
}
