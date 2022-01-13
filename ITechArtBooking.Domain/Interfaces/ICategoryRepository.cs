using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(Guid id);
        Task<Category> GetAsync(Guid id);
        Task CreateAsync(Category category);
        Task<Category> DeleteAsync(Guid id);
    }
}
