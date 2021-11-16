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

        public IEnumerable<Room> GetAll()
        {
            return Context.Rooms
                .Include(r => r.Category)
                .Include(h => h.Category.Hotel)
                .ToList();
        }

        public Room Get(Guid id)
        {
            return Context.Rooms
                .Include(r => r.Category)
                .Include(h => h.Category.Hotel)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Create(Room room)
        {
            Context.Rooms.Add(room);
            Context.SaveChanges();
        }

        public void Update(Room room)
        {
            Room currentRoom = Get(room.Id);

            currentRoom.Category = room.Category;
            currentRoom.Picture = room.Picture;

            Context.Rooms.Update(currentRoom);
            Context.SaveChanges();
        }

        public Room Delete(Guid id)
        {
            Room room = Get(id);

            if (room != null) {
                Context.Rooms.Remove(room);
                Context.SaveChanges();
            }

            return room;
        }
    }
}
