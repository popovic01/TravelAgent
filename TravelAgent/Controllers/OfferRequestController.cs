using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.OfferRequest;
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
        public ActionResult RequestOffer(int clientId, OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.RequestOffer(clientId, dataIn));
        }

        [HttpGet]
        public ActionResult GetAllRequestedOffers()
        {
            return Ok(_offerReqService.GetAllRequestedOffers());
        }

        [HttpGet("{id}")]
        public ActionResult GetRequestedOfferById(int offerReqId)
        {
            return Ok(_offerReqService.GetRequestedOfferById(offerReqId));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRequestedOffer(int clientId, OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.UpdateRequestedOffer(clientId, dataIn));
        }

        [HttpDelete("{clientId}/{offerReqId}")]
        public ActionResult DeleteRequestedOffer(int clientId, int offerReqId)
        {
            return Ok(_offerReqService.DeleteRequestedOffer(clientId, offerReqId));
        }
    }
}
