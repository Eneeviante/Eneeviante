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
    public class EFHotelRepository : IHotelRepository
    {
        private readonly EFBookingDBContext Context;

        public EFHotelRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return Context.Hotels;
        }

        public async Task<Hotel> GetAsync(Guid id)
        {
            return await Context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task CreateAsync(Hotel hotel)
        {
            Context.Hotels.Add(hotel);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel newHotel)
        {
            Hotel currentHotel = await GetAsync(newHotel.Id);

            currentHotel.Name = newHotel.Name;
            currentHotel.Description = newHotel.Description;
            currentHotel.StarNumber = newHotel.StarNumber;
            currentHotel.Rooms = newHotel.Rooms;
            
            Context.Hotels.Update(currentHotel);
            await Context.SaveChangesAsync();
        }

        public async Task<Hotel> DeleteAsync(Guid id)
        {
            Hotel hotel = await GetAsync(id);

            if (hotel != null) {
                Context.Hotels.Remove(hotel);
                await Context.SaveChangesAsync();
            }

            return hotel;
        }
    }
}
