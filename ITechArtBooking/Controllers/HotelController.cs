using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;

        public HotelController(IHotelRepository _hotelRepository)
        {
            hotelRepository = _hotelRepository;
        }

        /*Просмотреть список всех отелей*/
        [Authorize(Roles = "User")]
        [HttpGet(Name = "GetAllHotels")]
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await hotelRepository.GetAllAsync();
        }

        /*Добавить отель*/
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(string name, string description, int starNumber)
        {
            Hotel hotel = new Hotel { 
                Id = new Guid(),
                Name = name,
                Description = description,
                StarNumber = starNumber
            };

            await hotelRepository.CreateAsync(hotel);
            return CreatedAtRoute(new { id = hotel.Id }, hotel);
        }

        /*Удалить отель*/
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedHotel = await hotelRepository.DeleteAsync(id);

            if (deletedHotel == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedHotel);
        }
    }
}
