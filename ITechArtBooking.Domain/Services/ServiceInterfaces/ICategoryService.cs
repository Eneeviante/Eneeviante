using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(Guid id);
        Task<Category> CreateAsync(Guid hotelId, int bedsNumber,
            string description, float costPerDay);
        Task<Category> DeleteAsync(Guid id);
    }
}
