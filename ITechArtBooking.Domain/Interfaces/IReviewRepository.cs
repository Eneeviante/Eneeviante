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
        IEnumerable<Review> GetAll(Guid hotelId);
        Review Get(Guid id);
        void Create(Review review);
        void Update(Review review);
        Review Delete(Guid id);
    }
}
