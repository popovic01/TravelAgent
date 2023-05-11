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
                retVal.Message = $"Već postoji lokacija {location.Name}";
                retVal.Status = 409;
            }
            else
            {
                _context.Locations.Add(_mapper.Map<Location>(location));
                _context.SaveChanges();
                retVal.Message = $"Uspešno dodata lokacija {location.Name}";
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
                retVal.Message = $"Ne postoji lokacija sa id-jem {id}";
            }
            else
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
                retVal.Message = $"Uspešno obrisana lokacija {location.Name}";
            }
            return retVal;
        }

        public PaginationDataOut<LocationIdDTO> GetAll(PageInfo pageInfo)
        {
            PaginationDataOut<LocationIdDTO> retVal = new ();

            IQueryable<Location> locations = _context.Locations;
            retVal.Count = locations.Count();

            locations = locations
                .OrderByDescending(x => x.Id)
                .Skip(pageInfo.PageSize * (pageInfo.Page))
                .Take(pageInfo.PageSize);

            locations.ToList().ForEach(x => retVal.Data.Add(_mapper.Map<LocationIdDTO>(x)));
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
                retVal.Message = $"Ne postoji lokacija sa id-jem {id}";
            }
            else if (locationNameDb != null)
            {
                retVal.Message = $"Već postoji lokacija {location.Name}";
                retVal.Status = 409;
            }
            else
            {
                locationDb.Name = location.Name;
                _context.SaveChanges();
                retVal.Message = $"Uspešno izmenjena lokacija {location.Name}";
            }

            return retVal;
        }
    }
}
