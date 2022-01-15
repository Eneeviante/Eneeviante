using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync(int pageSize, int pageNumber);
        Task<Hotel> GetAsync(Guid id);
        Task CreateAsync(Hotel hotel);
        Task UpdateAsync(Hotel room);
        Task<Hotel> DeleteAsync(Guid id);
    }
}
