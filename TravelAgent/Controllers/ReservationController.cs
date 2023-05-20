using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Reservation;
using TravelAgent.Helpers;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("getAll/{id}")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetAll(PageInfo pageInfo, int id)
        {
            return Ok(_reservationService.GetAll(pageInfo, id));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin,client")]
        public ActionResult GetById(int id)
        {
            return Ok(_reservationService.Get(id));
        }

        [HttpPost]
        [AuthRole("Role", "client")]
        public ActionResult Add(ReservationDTO dataIn)
        {
            return Ok(_reservationService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin,client")]
        public ActionResult Delete(int id)
        {
            return Ok(_reservationService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, ReservationDTO dataIn)
        {
            return Ok(_reservationService.Update(id, dataIn));
        }
    }
}
