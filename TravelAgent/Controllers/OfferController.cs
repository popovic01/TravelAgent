using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Offer;
using TravelAgent.Helpers;
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

        [HttpPost("getWishlist/{id}")]
        [AuthRole("UserId", "id")]
        public ActionResult GetWishlist(int id)
        {
            return Ok(_offerService.GetWishlist(id));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_offerService.Get(id));
        }

        [HttpPost]
        [AuthRole("Role", "admin")]
        public ActionResult Add(OfferReviewDTO dataIn)
        {
            return Ok(_offerService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_offerService.Delete(id));
        }

        [HttpDelete("deleteLocation/{offerId}/{locationId}")]
        [AuthRole("Role", "admin")]
        public ActionResult DeleteLocationForOffer(int offerId, int locationId)
        {
            return Ok(_offerService.DeleteLocationForOffer(offerId, locationId));
        }

        [HttpDelete("deleteTag/{offerId}/{tagId}")]
        [AuthRole("Role", "admin")]
        public ActionResult DeleteTagForOffer(int offerId, int tagId)
        {
            return Ok(_offerService.DeleteTagForOffer(offerId, tagId));
        }

        [HttpGet("addToWishlist/{offerId}/{clientId}")]
        [AuthRole("Role", "client")]
        public ActionResult AddOfferToWishlist(int offerId, int clientId)
        {
            return Ok(_offerService.AddOfferToWishlist(offerId, clientId));
        }

        [HttpGet("removeFromWishlist/{offerId}/{clientId}")]
        [AuthRole("Role", "client")]
        public ActionResult RemoveOfferFromWishlist(int offerId, int clientId)
        {
            return Ok(_offerService.RemoveOfferFromWishlist(offerId, clientId));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, OfferReviewDTO dataIn)
        {
            return Ok(_offerService.Update(id, dataIn));
        }

    }
}
