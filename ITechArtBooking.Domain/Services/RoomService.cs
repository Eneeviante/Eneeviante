using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Domain.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IHotelRepository hotelRepository;

        public RoomService(IRoomRepository _roomRepository,
            ICategoryRepository _categoryRepository, IHotelRepository _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            roomRepository = _roomRepository;
            hotelRepository = _hotelRepository;
        }

        public async Task<IEnumerable<Room>> GetAllByUserAsync(Guid userId)
        {
            
            return await roomRepository.GetAllByUserAsync(userId);
        }

        public async Task<IEnumerable<Room>> GetAllFreeInHotelAsync(Guid hotelId, int pageSize, int pageNumber)
        {
            var hotel = await hotelRepository.GetAsync(hotelId);
            if(hotel == null) {
                return null;
            }

            return await roomRepository.GetAllFreeInHotelAsync(hotelId, pageSize, pageNumber);
        }

        public async Task<Room> CreateAsync(Guid categoryId, short number, string picture)
        {
            var category = await categoryRepository.GetAsync(categoryId);
            if (category == null) {
                return null;
            }
            var hotel = await hotelRepository.GetAsync(category.Hotel.Id);

            Room newRoom = new Room {
                Id = new Guid(),
                Number = number,
                Category = category,
                Picture = picture
            };

            hotel.Rooms.Add(newRoom);

            await roomRepository.CreateAsync(newRoom);

            return newRoom;
        }

        public async Task<Room> DeleteAsync(Guid roomId)
        {
            return await roomRepository.DeleteAsync(roomId);
        }
    }
}
