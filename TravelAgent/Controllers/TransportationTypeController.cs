using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.TransportationType;
using TravelAgent.Helpers;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/transportationType")]
    [ApiController]
    public class TransportationTypeController : ControllerBase
    {
        private readonly ITransportationTypeService _transportationTypeService;

        public TransportationTypeController(ITransportationTypeService transportationTypeService)
        {
            _transportationTypeService = transportationTypeService;
        }

        [HttpPost("getAll")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetAll(PageInfo pageInfo)
        {
            return Ok(_transportationTypeService.GetAll(pageInfo));
        }

        [HttpPost]
        [AuthRole("Role", "admin")]
        public ActionResult Add(TransportationTypeDTO dataIn)
        {
            return Ok(_transportationTypeService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_transportationTypeService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, TransportationTypeDTO dataIn)
        {
            return Ok(_transportationTypeService.Update(id, dataIn));
        }
    }
}
