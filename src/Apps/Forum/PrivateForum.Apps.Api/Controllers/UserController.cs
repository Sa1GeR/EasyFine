using PrivateForum.App.Web.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (!(HttpContext?.User?.Identity?.IsAuthenticated ?? false))
                return Unauthorized();

            var result = await _userService.GetCurrentUserAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var result = await _userService.GetUserProfile(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var result = await _userService.BlockUser(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("upload-avatar/{id}")]
        public async Task<IActionResult> UploadAvatar(int id, IFormFile file)
        {
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                        var fileName = file.FileName;

                        var result = await _userService.UploadAvatarAsync(id, fileName, stream);

                        if (!result)
                        {
                            return NotFound();
                        }

                        return Ok();
                    }
                }
            }

            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}