using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ITechArtBooking.Helper;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        /*Просмотреть список всех пользователей в системе*/
        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAllUsers")]
        public async Task<IQueryable> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        /*Удалить свой аккаунт*/
        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var userId = User.GetUserId();

            var deletedUser = await userRepository.DeleteAsync(userId);

            if (deletedUser == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedUser);
        }
    }
}
