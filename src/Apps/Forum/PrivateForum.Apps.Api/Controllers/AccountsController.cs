using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                return Ok(GenerateJwtToken(user.Email, lookup));

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

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HnczRNSZ96EMCPw6H6nJQw2tbTDHIFJVknKxcN59RfPjuOE7xzs8ZwZfkQEyY0e"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(14));

            var token = new JwtSecurityToken(
                "http://localhost:1488",
                "http://localhost:1488",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}