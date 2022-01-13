using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync(Guid hotelId);
        Task<Review> GetAsync(Guid id);
        Task CreateAsync(Review review);
        Task UpdateAsync(Review review);
        Task<Review> DeleteAsync(Guid id);
    }
}