using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.OfferType;
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
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_offerTypeService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_offerTypeService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(OfferTypeDTO dataIn)
        {
            return Ok(_offerTypeService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_offerTypeService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, OfferTypeDTO dataIn)
        {
            return Ok(_offerTypeService.Update(id, dataIn));
        }
    }
}
