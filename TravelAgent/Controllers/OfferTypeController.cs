using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferType;
using TravelAgent.Helpers;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/offerType")]
    [ApiController]
    public class OfferTypeController : ControllerBase
    {
        private readonly IOfferTypeService _offerTypeService;

        public OfferTypeController(IOfferTypeService offerTypeService)
        {
            _offerTypeService = offerTypeService;
        }

        [HttpPost("getAll")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetAll(FilterParamsDTO filterParams)
        {
            return Ok(_offerTypeService.GetAll(filterParams));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult GetById(int id)
        {
            return Ok(_offerTypeService.Get(id));
        }

        [HttpPost]
        [AuthRole("Role", "admin")]
        public ActionResult Add(OfferTypeDTO dataIn)
        {
            return Ok(_offerTypeService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_offerTypeService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, OfferTypeDTO dataIn)
        {
            return Ok(_offerTypeService.Update(id, dataIn));
        }
    }
}
