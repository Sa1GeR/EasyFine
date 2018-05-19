using PrivateForum.App.Web.Configuration;
using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.App.Web.Services.Models.Forum;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/forum")]
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpGet("get/root")]
        public async Task<IActionResult> Root()
        {
            var result = await this._forumService.GetRootForum();

            return result == null ? NotFound() as IActionResult: Ok(result) as IActionResult;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _forumService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateForumVM forum)
        {
            var result = await _forumService.Create(forum);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]EditForumVM forum)
        {
            var result = await _forumService.Update(forum);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _forumService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}