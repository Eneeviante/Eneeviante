using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITechArtBooking.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ITechArtBooking.Helper;
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService _bookingService)
        {
            bookingService = _bookingService;
        }

        /*Просмотреть информацию о бронировании номеров в отелях*/
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageSize = 2, int pageNumber = 1)
        {
            var bookings = await bookingService.GetAllAsync(pageSize, pageNumber);

            if (bookings == null) {
                return BadRequest("Invalid page number");
            }

            return new ObjectResult(bookings);
        }

        /*Бронировать номер в отеле на определенный срок*/
        [Authorize(Roles = "User")]
        [HttpPost("{dateFrom}, {dateTo}, {roomId}")]
        public async Task<IActionResult> CreateAsync(string dateFrom, string dateTo, Guid roomId)
        {
            if(dateFrom == null || dateTo == null) {
                return BadRequest("All fields must be filled");
            }

            var userId = User.GetUserId();

            try {
                var dFrom = DateTime.Parse(dateFrom);
                var dTo = DateTime.Parse(dateTo);

                var newBooking = await bookingService.CreateAsync(userId, dFrom, dTo, roomId);
                if(newBooking == null) {
                    return BadRequest("Incorrect user or room id");
                }
                return CreatedAtRoute(new { id = newBooking.Id }, newBooking);
            }
            catch(FormatException) {
                return BadRequest("Incorrect date format");
            }
        }

        /*Отменять бронирование номер не позднее, чем за 5 дней*/
        [Authorize(Roles = "User")]
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteAsync(Guid bookingId)
        {
            var deletedBooking = await bookingService.DeleteAsync(bookingId);

            if (deletedBooking == null) {
                return BadRequest("The booking does not exist or cancellation is not possible");
            }

            return new ObjectResult(deletedBooking);
        }
    }
}
