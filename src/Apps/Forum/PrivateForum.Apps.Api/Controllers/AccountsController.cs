using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrivateForum.Apps.Services.Mappers;
using PrivateForum.Apps.Services.Models.Identity;
using PrivateForum.Apps.Services.Models.Identity.VM;

namespace PrivateForum.Apps.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountsController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginVM user)
        {
            var lookup = await _signInManager.UserManager.FindByEmailAsync(user.Email);

            if (lookup == null)
                return NotFound();

            var signInResult = await _signInManager.PasswordSignInAsync(lookup, user.Password, user.RememberMe, lockoutOnFailure: false);

            if (signInResult.Succeeded)
                return Ok(lookup);

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterVM register)
        {
            if (!register.Validate())
                return BadRequest(new ArgumentException("Registration model is not valid!"));

            var result = await _userManager.CreateAsync(register.ToDomain(), register.Password);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }
    }
}