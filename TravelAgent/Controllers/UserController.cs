using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.User;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Register(UserRequestDTO dataIn)
        {
            return Ok(_userService.Register(dataIn));
        }

        [HttpPost("login")]
        public ActionResult Login(UserRequestDTO dataIn)
        {
            return Ok(_userService.Login(dataIn));
        }

        [HttpPost("logout")]
        public ActionResult Logout(UserRequestDTO dataIn)
        {
            return Ok(_userService.Logout(dataIn));
        }

        [HttpPost("getAll")]
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_userService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_userService.Get(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, UserRequestDTO dataIn)
        {
            return Ok(_userService.Update(id, dataIn));
        }
    }
}
