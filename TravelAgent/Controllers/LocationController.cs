using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("getAll")]
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_locationService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_locationService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(LocationDTO dataIn)
        {
            return Ok(_locationService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_locationService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, LocationDTO dataIn)
        {
            return Ok(_locationService.Update(id, dataIn));
        }
    }
}
