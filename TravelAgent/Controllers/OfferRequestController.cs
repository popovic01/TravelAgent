using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.OfferRequest;
using TravelAgent.Helpers;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/offerRequest")]
    [ApiController]
    public class OfferRequestController : ControllerBase
    {
        private readonly IOfferRequestService _offerReqService;

        public OfferRequestController(IOfferRequestService offerReqService)
        {
            _offerReqService = offerReqService;
        }

        [HttpPost]
        [AuthRole("Role", "client")]
        public ActionResult RequestOffer(int clientId, OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.RequestOffer(clientId, dataIn));
        }

        [HttpGet]
        [AuthRole("Role", "admin")]
        public ActionResult GetAllRequestedOffers()
        {
            return Ok(_offerReqService.GetAllRequestedOffers());
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult GetRequestedOfferById(int offerReqId)
        {
            return Ok(_offerReqService.GetRequestedOfferById(offerReqId));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "client")]
        public ActionResult UpdateRequestedOffer(int clientId, OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.UpdateRequestedOffer(clientId, dataIn));
        }

        [HttpDelete("{clientId}/{offerReqId}")]
        [AuthRole("Role", "admin")]
        public ActionResult DeleteRequestedOffer(int clientId, int offerReqId)
        {
            return Ok(_offerReqService.DeleteRequestedOffer(clientId, offerReqId));
        }
    }
}
