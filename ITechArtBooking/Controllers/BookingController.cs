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
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> bookingRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Room> roomRepository;

        public BookingController(IRepository<Booking> _bookingRepository,
            IRepository<User> _userRepository, IRepository<Room> _roomRepository)
        {
            bookingRepository = _bookingRepository;
            userRepository = _userRepository;
            roomRepository = _roomRepository;
        }

        [HttpGet(Name = "GetAllBookings")]
        public IEnumerable<Booking> GetAll()
        {
            return bookingRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetBooking")]
        public IActionResult Get(Guid id)
        {
            Booking booking = bookingRepository.Get(id);

            if (booking == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(booking);
            }
        }

        [HttpPost]
        public IActionResult Create(string dateFrom, string dateTo,
            Guid userId, Guid roomId)
        {
            var user = userRepository.Get(userId);
            if (user == null) {
                return BadRequest();
            }
            var room = roomRepository.Get(roomId);
            if (room == null) {
                return BadRequest();
            }

            try {
                var costDay = room.Category.CostPerDay;
                var dFrom = DateTime.Parse(dateFrom);
                var dTo = DateTime.Parse(dateTo);
                var countDays = (dTo - dFrom).Duration();
                var sum = costDay * countDays.Days;

                Booking newBooking = new Booking {
                    Id = new Guid(),
                    DateFrom = dFrom,
                    DateTo = dTo,
                    User = user,
                    Room = room,
                    Sum = sum
                };

                bookingRepository.Create(newBooking);
                return CreatedAtRoute("GetBooking", new { id = newBooking.Id }, newBooking);
            }
            catch(InvalidCastException e) {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, string dateFrom, string dateTo,
            Guid userId, Guid roomId)
        {
            var newUser = userRepository.Get(userId);
            if (newUser == null) {
                return BadRequest();
            }

            var newRoom = roomRepository.Get(roomId);
            if (newRoom == null) {
                return BadRequest();
            }

            var oldBooking= bookingRepository.Get(id);
            if (oldBooking == null) {
                return NotFound("OldBooking not found");
            }

            Booking newBooking = new Booking {
                Id = id,
                DateFrom = DateTime.Parse(dateFrom),
                DateTo = DateTime.Parse(dateTo),
                User = newUser,
                Room = newRoom
            };

            bookingRepository.Update(newBooking);
            return RedirectToRoute("GetAllBookings");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedBooking = bookingRepository.Delete(id);

            if (deletedBooking == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedBooking);
        }
    }
}
