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

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly UserService postsService = new(new UsersFakeRepository());
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpGet(Name = "GetAllUsers")]
        public IQueryable GetAll()
        {
            return userRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(Guid id)
        {
            User user = userRepository.Get(id);

            if(user == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(user);
            }
        }

        [HttpPost]
        public IActionResult Create(string firstName, string middleName,
            string lastName, string phoneNumber)
        {
            User user = new User { 
                Id = new Guid(),
                FirstName = firstName,
                MiddleName =middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            if (user == null) {
                return BadRequest();
            }
            else {
                userRepository.Create(user);
                return CreatedAtRoute("GetUser", new { id = user.Id }, user);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, string firstName, string middleName,
            string lastName, string phoneNumber)
        {
            var user = userRepository.Get(id);
            if (user == null) {
                return NotFound();
            }

            var newUser = new User {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            userRepository.Update(newUser);
            return RedirectToRoute("GetAllUsers");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedUser = userRepository.Delete(id);

            if (deletedUser == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedUser);
        }
    }
}
