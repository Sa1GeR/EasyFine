using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.App.Web.Services.Models.Topic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/topic")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _topicService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateTopicVM topic)
        {
            var result = await _topicService.Create(topic);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]EditTopicVM topic)
        {
            var result = await _topicService.Update(topic);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _topicService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}