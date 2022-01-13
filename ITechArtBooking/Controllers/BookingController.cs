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
using System.Security.Claims;
using ITechArtBooking.Helper;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoomRepository roomRepository;

        public BookingController(IBookingRepository _bookingRepository,
            IUserRepository _userRepository, IRoomRepository _roomRepository)
        {
            bookingRepository = _bookingRepository;
            userRepository = _userRepository;
            roomRepository = _roomRepository;
        }

        /*Просмотреть информацию о бронировании номеров в отелях*/
        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetBookings")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookings = await bookingRepository.GetAllAsync();

            if (bookings == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(bookings);
            }
        }

        /*Бронировать номер в отеле на определенный срок*/
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string dateFrom, string dateTo, Guid roomId)
        {
            var userId = User.GetUserId();
            var user = await userRepository.GetAsync(userId);
            if (user == null) {
                return BadRequest();
            }
            var room = await roomRepository.GetAsync(roomId);
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

                room.LastBooking = newBooking;
                await bookingRepository.CreateAsync(newBooking);
                return CreatedAtRoute(new { id = newBooking.Id }, newBooking);
            }
            catch(InvalidCastException e) {
                return BadRequest(e);
            }
        }

        /*Отменять бронирование номер не позднее, чем за 5 дней*/
        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var booking = await bookingRepository.GetAsync(id);
            if((booking.DateFrom - DateTime.Now).TotalDays < 5) {
                return BadRequest("Unable to delete, less than 5 days left");
            }

            var deletedBooking = await bookingRepository.DeleteAsync(id);

            if (deletedBooking == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedBooking);
        }
    }
}
