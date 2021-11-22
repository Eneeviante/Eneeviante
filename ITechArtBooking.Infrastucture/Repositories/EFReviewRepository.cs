using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFReviewRepository : IRepository<Review>
    {
        private readonly EFBookingDBContext Context;

        public EFReviewRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return Context.Reviews
                .Include(r => r.Client)
                .Include(r => r.Hotel).ToList();
        }

        public Review Get(Guid id)
        {
            return Context.Reviews
                .Include(r=>r.Hotel)
                .Include(r=>r.Client)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Create(Review review)
        {
            Context.Reviews.Add(review);
            Context.SaveChanges();
        }

        public void Update(Review review)
        {
            Review currentReview = Get(review.Id);

            currentReview.Client = review.Client;
            currentReview.Hotel = review.Hotel;
            currentReview.Text = review.Text;

            Context.Reviews.Update(currentReview);
            Context.SaveChanges();
        }

        public Review Delete(Guid id)
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
