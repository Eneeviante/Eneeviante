using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllAsync(int pageSize, int pageNumber);
        Task<Hotel> CreateAsync(string name, string description, int starNumber);
        Task<Hotel> DeleteAsync(Guid id);
    }
}
