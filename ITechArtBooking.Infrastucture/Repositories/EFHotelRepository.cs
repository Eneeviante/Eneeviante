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
    public class EFHotelRepository : IRepository<Hotel>
    {
        private readonly EFBookingDBContext Context;

        public EFHotelRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return Context.Hotels
                .Include(h => h.Rooms);
        }

        public Hotel Get(Guid id)
        {
            return Context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.Id == id);
        }

        public void Create(Hotel hotel)
        {
            Context.Hotels.Add(hotel);
            Context.SaveChanges();
        }

        public void Update(Hotel newHotel)
        {
            Hotel currentHotel = Get(newHotel.Id);

            currentHotel.Name = newHotel.Name;
            currentHotel.Description = newHotel.Description;
            currentHotel.StarNumber = newHotel.StarNumber;
            currentHotel.Rooms = newHotel.Rooms;
            
            Context.Hotels.Update(currentHotel);
            Context.SaveChanges();
        }

        public Hotel Delete(Guid id)
        {
            Hotel hotel = Get(id);

            if (hotel != null) {
                Context.Hotels.Remove(hotel);
                Context.SaveChanges();
            }

            return hotel;
        }
    }
}
