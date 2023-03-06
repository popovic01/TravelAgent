using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.TransportationType;
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
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_transportationTypeService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_transportationTypeService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(TransportationTypeDTO dataIn)
        {
            return Ok(_transportationTypeService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_transportationTypeService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, TransportationTypeDTO dataIn)
        {
            return Ok(_transportationTypeService.Update(id, dataIn));
        }
    }
}
