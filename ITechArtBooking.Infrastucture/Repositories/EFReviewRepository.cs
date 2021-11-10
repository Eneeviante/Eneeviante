using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly EFBookingDBContext Context;

        public EFReviewRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return Context.Reviews;
        }

        public Review Get(long id)
        {
            return Context.Reviews.Find(id);
        }

        public void Create(Review review)
        {
            Context.Reviews.Add(review);
            Context.SaveChanges();
        }

        public void Update(Review review)
        {
            Review currentReview = Get(review.Id);

            currentReview.ClientId = review.ClientId;
            currentReview.HotelId = review.HotelId;
            currentReview.Text = review.Text;

            Context.Reviews.Update(currentReview);
            Context.SaveChanges();
        }

        public Review Delete(long id)
        {
            Review review = Get(id);

            if (review != null) {
                Context.Reviews.Remove(review);
                Context.SaveChanges();
            }

            return review;
        }
    }
}
