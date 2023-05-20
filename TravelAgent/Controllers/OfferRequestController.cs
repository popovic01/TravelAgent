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
        public ActionResult RequestOffer(OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.RequestOffer(dataIn));
        }

        [HttpPost("getAll")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetAllRequestedOffers(RequestDTO pageInfo)
        {
            return Ok(_offerReqService.GetAllRequestedOffers(pageInfo));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetRequestedOfferById(int id)
        {
            return Ok(_offerReqService.GetRequestedOfferById(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "client")]
        public ActionResult UpdateRequestedOffer(int id, OfferRequestDTO dataIn)
        {
            return Ok(_offerReqService.UpdateRequestedOffer(id, dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin,client")]
        public ActionResult DeleteRequestedOffer(int id)
        {
            return Ok(_offerReqService.DeleteRequestedOffer(id));
        }
    }
}
