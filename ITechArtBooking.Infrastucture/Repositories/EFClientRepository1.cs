using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFHotelRepository : IHotelRepository
    {
        private readonly EFBookingDBContext Context;

        public EFHotelRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return Context.Hotels;
        }

        public Hotel Get(long id)
        {
            return Context.Hotels.Find(id);
        }

        public void Create(Hotel hotel)
        {
            Context.Hotels.Add(hotel);
            Context.SaveChanges();
        }

        public void Update(Hotel updatedClient)
        {
            Hotel currentHotel = Get((int)updatedClient.Id);

            currentHotel.Name = updatedClient.Name;
            currentHotel.StarNumber = updatedClient.StarNumber;

            Context.Hotels.Update(currentHotel);
            Context.SaveChanges();
        }

        public Hotel Delete(long id)
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
