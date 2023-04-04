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

        [HttpDelete("deleteLocation/{offerId}/{locationId}")]
        public ActionResult DeleteLocationForOffer(int offerId, int locationId)
        {
            return Ok(_offerService.DeleteLocationForOffer(offerId, locationId));
        }

        [HttpDelete("deleteTag/{offerId}/{tagId}")]
        public ActionResult DeleteTagForOffer(int offerId, int tagId)
        {
            return Ok(_offerService.DeleteTagForOffer(offerId, tagId));
        }

        [HttpGet("addToWishlist/{offerId}/{clientId}")]
        public ActionResult AddOfferToWishlist(int offerId, int clientId)
        {
            return Ok(_offerService.AddOfferToWishlist(offerId, clientId));
        }

        [HttpGet("removeFromWishlist/{offerId}/{clientId}")]
        public ActionResult RemoveOfferFromWishlist(int offerId, int clientId)
        {
            return Ok(_offerService.RemoveOfferFromWishlist(offerId, clientId));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, OfferReviewDTO dataIn)
        {
            return Ok(_offerService.Update(id, dataIn));
        }

    }
}
