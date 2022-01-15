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
    public class EFRoomRepository : IRoomRepository
    {
        private readonly EFBookingDBContext Context;

        public EFRoomRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Room>> GetAllByUserAsync(Guid userId)
        {
            return await Context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Category)
                .Where(b => b.User.Id == userId)
                .Select(b => b.Room)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllFreeInHotelAsync(Guid hotelId)
        {
            return await Context.Rooms
                .Include(r => r.Category)
                .Where(r => r.Category.Hotel.Id == hotelId)
                .Where(r => r.LastBooking == null || r.LastBooking.DateTo <= DateTime.Now)
                .ToListAsync();
        }

        public async Task<Room> GetAsync(Guid id)
        {
            return await Context.Rooms
                .Include(r => r.Category)
                .Include(h => h.Category.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateAsync(Room room)
        {
            Context.Rooms.Add(room);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            Room currentRoom = await GetAsync(room.Id);

            currentRoom.Category = room.Category;
            currentRoom.Picture = room.Picture;

            Context.Rooms.Update(currentRoom);
            await Context.SaveChangesAsync();
        }

        public async Task<Room> DeleteAsync(Guid id)
        {
            Room room = await GetAsync(id);

            if (room != null) {
                var transaction = await Context.Database.BeginTransactionAsync();
                try {
                    System.IO.File.Delete(room.Picture);

                    Context.Rooms.Remove(room);
                    await Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception) {
                    return null;
                }
            }

            return room;
        }
    }
}
