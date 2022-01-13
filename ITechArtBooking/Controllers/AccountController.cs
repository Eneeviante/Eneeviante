using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AccountsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register(string email, string firstName,
            string middleName, string lastName, string password)
        {
            var user = new User {
                Email = email,
                UserName = email,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(user);
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) {
                return Unauthorized("No such User");
            }
            if (!await _userManager.CheckPasswordAsync(user, password)) {
                return Unauthorized("Wrong Pass");
            }
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "")
            };


            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("aksdokjafbkjasbfjabojsfbda"));

            var token = new JwtSecurityToken(
                issuer: "test",
                audience: "test",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey,
                        SecurityAlgorithms.HmacSha256)
                );

            var result = new {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiration = token.ValidTo
            };

            return Ok(result);
        }
    }
}
