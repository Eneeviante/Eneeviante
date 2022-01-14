using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> CreateAsync(Guid userId, DateTime dateFrom,
            DateTime dateTo, Guid roomId);
        Task<Booking> DeleteAsync(Guid id);
    }
}
