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
        //private readonly ClientService postsService = new(new ClientsFakeRepository());
        private readonly IRoomRepository roomRepository;
        private readonly ICategoryRepository categoryRepository;

        public RoomController(IRoomRepository _roomRepository,
            ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
            roomRepository = _roomRepository;
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
        public IActionResult Create(Guid categoryId, string picture)
        {
            var category = categoryRepository.Get(categoryId);
            if (category == null) {
                return BadRequest();
            }

            Room newRoom = new Room {
                Id = new Guid(),
                Category = category,
                Picture = picture
            };

            roomRepository.Create(newRoom);
            return CreatedAtRoute("GetRoom", new { id = newRoom.Id }, newRoom);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Guid categoryId, string picture)
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
