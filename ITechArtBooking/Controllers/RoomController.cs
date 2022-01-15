using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITechArtBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using ITechArtBooking.Helper;
using ITechArtBooking.Domain.Services.ServiceInterfaces;
using System.Drawing;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService _roomService)
        {
            roomService = _roomService;
        }

        /*Просматривать информацию о своих забронированных номерах*/
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllByUserAsync()
        {
            var userId = User.GetUserId();
            return await roomService.GetAllByUserAsync(userId);
        }

        /*Просматривать список свободных номеров в отеле*/
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetAllFreeInHotelAsync(Guid hotelId, int pageSize = 2, int pageNumber = 1)
        {
            var rooms = await roomService.GetAllFreeInHotelAsync(hotelId, pageSize, pageNumber);

            if (rooms == null) {
                return BadRequest("Invalid hotel id or page number");
            }

            return new ObjectResult(rooms);
        }

        /*Добавить номер в отель*/
        [Authorize(Roles = "Admin")]
        [HttpPost("{categoryId}, {number}, {picture}")]
        public async Task<IActionResult> CreateAsync(Guid categoryId, short number, string picture)
        {
            if (!System.IO.File.Exists(picture)) {
                return BadRequest("Invalid path to the picture or file does not exist");
            }

            var newRoom = await roomService.CreateAsync(categoryId, number, picture);
            if(newRoom == null) {
                return BadRequest("Invalid category id");
            }
            return CreatedAtRoute(new { id = newRoom.Id }, newRoom);
        }

        /*Удалить номер из отеля*/
        [Authorize(Roles = "Admin")]
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteAsync(Guid roomId)
        {
            var deletedRoom = await roomService.DeleteAsync(roomId);

            if (deletedRoom == null) {
                return BadRequest("Invalid room id or couldn't delete image");
            }

            return new ObjectResult(deletedRoom);
        }
    }
}
