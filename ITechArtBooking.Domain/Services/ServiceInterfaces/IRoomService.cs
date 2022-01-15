using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllByUserAsync(Guid userId);
        Task<IEnumerable<Room>> GetAllFreeInHotelAsync(Guid hotelId, int pageSize, int pageNumber);
        Task<Room> CreateAsync(Guid categoryId, short number, string picture);
        Task<Room> DeleteAsync(Guid roomId);
    }
}
