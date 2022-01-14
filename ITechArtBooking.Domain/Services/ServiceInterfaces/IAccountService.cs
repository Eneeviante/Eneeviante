using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<User> Register(string email, string firstName,
            string middleName, string lastName, string password);
        Task<dynamic> Login(string email, string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> DeleteAsync(Guid userId);
    }
}
