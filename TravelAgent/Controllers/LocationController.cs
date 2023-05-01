using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;
using TravelAgent.Helpers;
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
        //[AuthRole("Role", "admin,client")]
        public ActionResult GetAll(FilterParamsDTO filterParams)
        {
            return Ok(_locationService.GetAll(filterParams));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult GetById(int id)
        {
            return Ok(_locationService.Get(id));
        }

        [HttpPost]
        [AuthRole("Role", "admin")]
        public ActionResult Add(LocationDTO dataIn)
        {
            return Ok(_locationService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_locationService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, LocationDTO dataIn)
        {
            return Ok(_locationService.Update(id, dataIn));
        }
    }
}
