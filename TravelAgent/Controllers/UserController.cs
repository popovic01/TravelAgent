using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.User;
using TravelAgent.Helpers;
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
        public ActionResult Login(UserLoginDTO dataIn)
        {
            return Ok(_userService.Login(dataIn));
        }

        [HttpPost("getAll")]
        [AuthRole("Role", "admin")]
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_userService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "client")]
        public ActionResult GetById(int id)
        {
            return Ok(_userService.Get(id));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "client")]
        public ActionResult Put(int id, UserRequestDTO dataIn)
        {
            return Ok(_userService.Update(id, dataIn));
        }
    }
}
