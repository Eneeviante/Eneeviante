using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFBookingRepository : IBookingRepository
    {
        private readonly EFBookingDBContext Context;

        public EFBookingRepository(EFBookingDBContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Booking>> GetAllAsync(int pageSize, int pageNumber)
        {
            var bookings = await  Context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Category)
                .Include(b => b.User)
                .ToListAsync();

            PageModel<Booking> pageModel = new PageModel<Booking>(bookings, pageNumber, pageSize);
            if (!pageModel.IsCorrectPage()) {
                return null;
            }
            return pageModel.ItemsOnPage();
        }

        public async Task<Booking> GetAsync(Guid id)
        {
            return await Context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Category)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateAsync(Booking booking)
        {
            Context.Bookings.Add(booking);
            await Context.SaveChangesAsync();
        }

        public async Task<Booking> DeleteAsync(Guid id)
        {
            Booking booking = await GetAsync(id);

            if (booking != null) {
                Context.Bookings.Remove(booking);
                await Context.SaveChangesAsync();
            }

            return booking;
        }
    }
}
