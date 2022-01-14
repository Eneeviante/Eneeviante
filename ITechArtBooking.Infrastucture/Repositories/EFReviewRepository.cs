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
    public class EFReviewRepository : IReviewRepository
    {
        private readonly EFBookingDBContext Context;

        public EFReviewRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(Guid hotelId)
        {
            return await Context.Reviews
                .Include(r => r.User)
                .Include(r => r.Hotel)
                .Where(r => r.Hotel.Id == hotelId)
                .ToListAsync();
        }

        public async Task<Review> GetAsync(Guid id)
        {
            return await Context.Reviews
                .Include(r=>r.Hotel)
                .Include(r=>r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateAsync(Review review)
        {
            Context.Reviews.Add(review);
            await Context.SaveChangesAsync();
        }

        public async Task<Review> DeleteAsync(Guid id)
        {
            Review review = await GetAsync(id);

            if (review != null) {
                Context.Reviews.Remove(review);
                await Context.SaveChangesAsync();
            }

            return review;
        }
    }
}
