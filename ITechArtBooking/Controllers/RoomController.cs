using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        //private readonly UserService postsService = new(new UsersFakeRepository());
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Hotel> hotelRepository;

        public RoomController(IRepository<Room> _roomRepository,
            IRepository<Category> _categoryRepository, IRepository<Hotel> _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            roomRepository = _roomRepository;
            hotelRepository = _hotelRepository;
        }

        [HttpGet(Name = "GetAllRooms")]
        public IEnumerable<Room> GetAll()
        {
            return roomRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetRoom")]
        public IActionResult Get(Guid id)
        {
            Room room = roomRepository.Get(id);

            if (room == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(room);
            }
        }

        [HttpPost]
        public IActionResult Create(Guid categoryId, short number, string picture)
        {
            var category = categoryRepository.Get(categoryId);
            if (category == null) {
                return NotFound();
            }
            var hotel = hotelRepository.Get(category.Hotel.Id);

            Room newRoom = new Room {
                Id = new Guid(),
                Number = number,
                Category = category,
                Picture = picture
            };

            hotel.Rooms.Add(newRoom);

            roomRepository.Create(newRoom);

            return CreatedAtRoute("GetRoom", new { id = newRoom.Id }, newRoom);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, short number, Guid categoryId, string picture)
        {
            var oldRoom = roomRepository.Get(id);
            if (oldRoom == null) {
                return BadRequest();
            }

            var newCategory = categoryRepository.Get(categoryId);
            if (newCategory == null) {
                return NotFound();
            }

            var newRoom = new Room {
                Id = id,
                Number = number,
                Category = newCategory,
                Picture = picture
            };

            roomRepository.Update(newRoom);
            return RedirectToRoute("GetAllRooms");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedRoom = roomRepository.Delete(id);

            if (deletedRoom == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedRoom);
        }
    }
}
