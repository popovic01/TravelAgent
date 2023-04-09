using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelAgent.DTO.Reservation;
using TravelAgent.Helpers;
using TravelAgent.Model;
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

        [HttpPost("getAll")]
        [AuthRole("Role", "admin")]
        public ActionResult GetAll()
        {
            return Ok(_reservationService.GetAll());
        }

        [HttpPost("getAllByUser/{id}")]
        [AuthRole("UserId", "id")]
        public ActionResult GetAllByUser(int id)
        {
            return Ok(_reservationService.GetAllByUser(id));
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
