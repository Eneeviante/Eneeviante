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
        public IEnumerable<Review> GetAll();
        public Review Get(long id);
        void Create(Review category);
        void Update(Review category);
        Review Delete(long id);
    }
}
