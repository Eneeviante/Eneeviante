using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllByUserAsync(Guid userId);
        Task<IEnumerable<Room>> GetAllFreeInHotelAsync(Guid id, int pageSize, int pageNumber);
        Task<Room> GetAsync(Guid id);
        Task CreateAsync(Room room);
        Task UpdateAsync(Room room);
        Task<Room> DeleteAsync(Guid id);
    }
}
