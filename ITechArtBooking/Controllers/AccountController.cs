using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Services.ServiceInterfaces;
using ITechArtBooking.Helper;
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
        private readonly IAccountService accountService;

        public AccountsController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpPost("/register, {email}, {password}")]
        public async Task<ActionResult> Register(string email, string firstName,
            string middleName, string lastName, string password)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+.+.[a-z]{2,4}$"))
            {
                return BadRequest("Invalid email");
            }

            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$")) {
                return BadRequest("Password must be more complex");
            }

            var user = await accountService.Register(email, firstName, 
                middleName, lastName, password);
            if (user == null) {
                return BadRequest("Something wrong");
            }

            return Ok(user);
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var result = await accountService.Login(email, password);
            if(result == null) {
                return BadRequest("Invalid login or password");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return new ObjectResult(accountService.GetAllAsync());
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            Guid userId = User.GetUserId();
            var user = await accountService.DeleteAsync(userId);
            return new ObjectResult(user);
        }
    }
}
