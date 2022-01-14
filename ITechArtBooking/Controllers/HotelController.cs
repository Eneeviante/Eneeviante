using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService _hotelService)
        {
            hotelService = _hotelService;
        }

        /*Просмотреть список всех отелей*/
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await hotelService.GetAllAsync();
        }

        /*Добавить отель*/
        [Authorize(Roles = "Admin")]
        [HttpPost("{name}, {description}, {starNumber}")]
        public async Task<IActionResult> CreateAsync(string name, string description, int starNumber)
        {
            var newHotel = await hotelService.CreateAsync(name, description, starNumber);
            return CreatedAtRoute(new { id = newHotel.Id }, newHotel);
        }

        /*Удалить отель*/
        [Authorize(Roles = "Admin")]
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteAsync(Guid hotelId)
        {
            var deletedHotel = await hotelService.DeleteAsync(hotelId);

            if (deletedHotel == null) {
                return BadRequest("Invalid hotel id");
            }

            return new ObjectResult(deletedHotel);
        }
    }
}
