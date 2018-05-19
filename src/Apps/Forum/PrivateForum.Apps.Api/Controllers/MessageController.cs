using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.App.Web.Services.Models.Message;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateMessageVM message)
        {
            var result = await  _messageService.Create(message);

            if (result == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]EditMessageVM message)
        {
            var result = await _messageService.Update(message);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _messageService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}