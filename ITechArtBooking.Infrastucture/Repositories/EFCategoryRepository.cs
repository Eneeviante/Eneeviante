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
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFBookingDBContext Context;

        public EFCategoryRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid hotelId)
        {
            return await Context.Categories
                .Where(c => c.Hotel.Id == hotelId)
                .ToListAsync();
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await Context.Categories
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            Category category = await GetAsync(id);

            if (category != null) {
                Context.Categories.Remove(category);
                await Context.SaveChangesAsync();
            }

            return category;
        }
    }
}
