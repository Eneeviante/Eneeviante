using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Services.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync(Guid hotelId);
        Task<Review> CreateAsync(Guid userId, Guid hotelId, string text);
    }
}
