using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;

namespace ITechArtBooking.Domain.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly UserManager<User> userManager;
        private readonly IRoomRepository roomRepository;

        public BookingService(IBookingRepository _bookingRepository,
            UserManager<User> _userManager, IRoomRepository _roomRepository)
        {
            bookingRepository = _bookingRepository;
            userManager = _userManager;
            roomRepository = _roomRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync(int pageSize, int pageNumber)
        {
            return await bookingRepository.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<Booking> CreateAsync(Guid userId, DateTime dateFrom,
            DateTime dateTo, Guid roomId)
        {
            var user = await userManager.FindByIdAsync(roomId.ToString());
            var room = await roomRepository.GetAsync(roomId);
            if (user == null || room == null) {
                return null;
            }

            var costDay = room.Category.CostPerDay;
            var countDays = (dateTo - dateFrom).Duration();
            var sum = costDay * countDays.Days;

            Booking newBooking = new Booking {
                Id = new Guid(),
                DateFrom = dateFrom,
                DateTo = dateTo,
                User = user,
                Room = room,
                Sum = sum
            };

            room.LastBooking = newBooking;
            await bookingRepository.CreateAsync(newBooking);
            return newBooking;
        }

        public async Task<Booking> DeleteAsync(Guid bookingId)
        {
            var booking = await bookingRepository.GetAsync(bookingId);
            if(booking == null) {
                return null;
            }
            if ((booking.DateFrom - DateTime.Now).TotalDays < 5) {
                return null;
            }

            var deletedBooking = await bookingRepository.DeleteAsync(bookingId);
            return deletedBooking;
        }
    }
}
