using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Offer;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost("getAll")]
        public ActionResult GetAll(OfferPageInfo dataIn)
        {
            return Ok(_offerService.GetAll(dataIn));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_offerService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(OfferReviewDTO dataIn)
        {
            return Ok(_offerService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_offerService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, OfferReviewDTO dataIn)
        {
            return Ok(_offerService.Update(id, dataIn));
        }

    }
}
