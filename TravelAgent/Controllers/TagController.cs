using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Tag;
using TravelAgent.Helpers;
using TravelAgent.Services.Interfaces;

namespace TravelAgent.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("getAll")]
        //[AuthRole("Role", "admin,client")]
        public ActionResult GetAll(FilterParamsDTO filterParams)
        {
            return Ok(_tagService.GetAll(filterParams));
        }

        [HttpGet("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult GetById(int id)
        {
            return Ok(_tagService.Get(id));
        }

        [HttpPost]
        [AuthRole("Role", "admin")]
        public ActionResult Add(TagDTO dataIn)
        {
            return Ok(_tagService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Delete(int id)
        {
            return Ok(_tagService.Delete(id));
        }

        [HttpPut("{id}")]
        [AuthRole("Role", "admin")]
        public ActionResult Put(int id, TagDTO dataIn)
        {
            return Ok(_tagService.Update(id, dataIn));
        }
    }
}
