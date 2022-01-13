using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IQueryable> GetAllAsync();
        Task<User> GetAsync(Guid id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<User> DeleteAsync(Guid id);
    }
}
