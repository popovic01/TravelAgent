using Microsoft.AspNetCore.Mvc;
using TravelAgent.DTO.Common;
using TravelAgent.DTO.Location;
using TravelAgent.DTO.Tag;
using TravelAgent.Services.Implementations;
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
        public ActionResult GetAll(SearchDTO searchData)
        {
            return Ok(_tagService.GetAll(searchData));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_tagService.Get(id));
        }

        [HttpPost]
        public ActionResult Add(TagDTO dataIn)
        {
            return Ok(_tagService.Add(dataIn));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_tagService.Delete(id));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, TagDTO dataIn)
        {
            return Ok(_tagService.Update(id, dataIn));
        }
    }
}
