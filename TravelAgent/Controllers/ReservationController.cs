using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Reservation;
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
        public ActionResult GetAll()
        {
            return Ok(_reservationService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_reservationService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(ReservationDTO dataIn)
        {
            return Ok(_reservationService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_reservationService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ReservationDTO dataIn)
        {
            return Ok(_reservationService.Update(id, dataIn));
        }
    }
}
