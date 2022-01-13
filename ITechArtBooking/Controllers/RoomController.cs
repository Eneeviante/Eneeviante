using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class RoomController : ControllerBase
    {
        //private readonly UserService postsService = new(new UsersFakeRepository());
        private readonly IRoomRepository roomRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IHotelRepository hotelRepository;

        public RoomController(IRoomRepository _roomRepository,
            ICategoryRepository _categoryRepository, IHotelRepository _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            roomRepository = _roomRepository;
            hotelRepository = _hotelRepository;
        }

        /*Просматривать информацию о своих забронированных номерах*/
        [Authorize(Roles = "User")]
        [HttpGet(Name = "GetAllRoomsByUser")]
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            var userId = this.User.GetUserId();
            return await roomRepository.GetAllByUserAsync(userId);
        }

        /*Просматривать список свободных номеров в отеле*/
        [HttpGet("{id}", Name = "GetFreeRoomInHotel")]
        public async Task<IActionResult> GetAllFreeInHotelAsync(Guid id)
        {
            var rooms = await roomRepository.GetAllFreeInHotelAsync(id);

            if (rooms == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(rooms);
            }
        }

        /*Добавить номер в отель*/
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid categoryId, short number, string picture)
        {
            var category = await categoryRepository.GetAsync(categoryId);
            if (category == null) {
                return NotFound();
            }
            var hotel = await hotelRepository.GetAsync(category.Hotel.Id);
            if(hotel == null) {
                return NotFound();
            }

            Room newRoom = new Room {
                Id = new Guid(),
                Number = number,
                Category = category,
                Picture = picture
            };

            hotel.Rooms.Add(newRoom);

            await roomRepository.CreateAsync(newRoom);

            return CreatedAtRoute(new { id = newRoom.Id }, newRoom);
        }

        /*Удалить номер из отеля*/
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedRoom = await roomRepository.DeleteAsync(id);

            if (deletedRoom == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedRoom);
        }
    }
}
