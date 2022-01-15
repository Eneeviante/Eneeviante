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
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;

        public HotelService(IHotelRepository _hotelRepository)
        {
            hotelRepository = _hotelRepository;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync(int pageSize, int pageNumber)
        {
            return await hotelRepository.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<Hotel> CreateAsync(string name, string description, int starNumber)
        {
            Hotel hotel = new Hotel {
                Id = new Guid(),
                Name = name,
                Description = description,
                StarNumber = starNumber
            };

            await hotelRepository.CreateAsync(hotel);
            return hotel;
        }

        public async Task<Hotel> DeleteAsync(Guid hotelId)
        {
           return await hotelRepository.DeleteAsync(hotelId);
        }
    }
}
