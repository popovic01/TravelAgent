using AutoMapper;
using TravelAgent.AppDbContext;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;
using TravelAgent.Model;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponsePackageNoData Add(LocationDTO location)
        {
            var retVal = new ResponsePackageNoData();

            var locationDb = _context.Locations.FirstOrDefault(x => x.Name == location.Name);

            if (locationDb != null)
            {
                retVal.Message = $"Already exists Location {location.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.Locations.Add(_mapper.Map<Location>(location));
                _context.SaveChanges();
                retVal.Message = $"Successfully added Location {location.Name}";
            }

            return retVal;
        }

        public ResponsePackageNoData Delete(int id)
        {
            var retVal = new ResponsePackageNoData();

            var location = _context.Locations.FirstOrDefault(x => x.Id == id);

            if (location == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Location with ID {id}";
            }
            else
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
                retVal.Message = $"Successfully deleted Location {location.Name}";
            }
            return retVal;
        }

        public ResponsePackage<LocationDTO> Get(int id)
        {
            var retVal = new ResponsePackage<LocationDTO>();

            var location = _context.Locations
                .FirstOrDefault(x => x.Id == id);

            if (location == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Location with ID {id}";
            }
            else
                retVal.TransferObject = _mapper.Map<LocationDTO>(location);

            return retVal;
        }

        public PaginationDataOut<LocationDTO> GetAll(FilterParamsDTO filterParams)
        {
            PaginationDataOut<LocationDTO> retVal = new PaginationDataOut<LocationDTO>();

            IQueryable<Location> locations = _context.Locations;

            if (!string.IsNullOrWhiteSpace(filterParams.SearchFilter))
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(filterParams.SearchFilter));
            }
            retVal.Count = locations.Count();

            locations = locations
                .OrderByDescending(x => x.Id)
                .Skip(filterParams.PageSize * (filterParams.Page - 1))
                .Take(filterParams.PageSize);

            locations.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<LocationDTO>(x)));
            return retVal;
        }

        public ResponsePackageNoData Update(int id, LocationDTO location)
        {
            var retVal = new ResponsePackageNoData();

            var locationDb = _context.Locations
                .FirstOrDefault(x => x.Id == id);
            var locationNameDb = _context.Locations
                .FirstOrDefault(x => x.Name.ToLower() == location.Name.ToLower());

            if (locationDb == null)
            {
                retVal.Status = 404;
                retVal.Message = $"No Location with ID {id}";
            }
            else if (locationNameDb != null)
            {
                retVal.Message = $"Already exists Location {location.Name}";
                retVal.Status = 409;
            }
            else
            {
                locationDb.Name = location.Name;
                _context.SaveChanges();
                retVal.Message = $"Successfully updated Location {location.Name}";
            }

            return retVal;
        }
    }
}
