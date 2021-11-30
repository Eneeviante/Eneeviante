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
    public class EFBookingRepository : IRepository<Booking>
    {
        private readonly EFBookingDBContext Context;

        public EFBookingRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return Context.Bookings
                .Include(b => b.Client)
                .Include(b => b.Room)
                .ThenInclude(r => r.Category)
                .ThenInclude(c => c.Hotel)
                .ToList();
        }

        public Booking Get(Guid id)
        {
            return Context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Category)
                .Include(b => b.Client)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Create(Booking booking)
        {
            Context.Bookings.Add(booking);
            Context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            Booking currentBooking = Get(booking.Id);

            currentBooking.DateFrom = booking.DateFrom;
            currentBooking.DateTo = booking.DateTo;
            currentBooking.Room = booking.Room;
            currentBooking.Client = booking.Client;

            Context.Bookings.Update(currentBooking);
            Context.SaveChanges();
        }

        public Booking Delete(Guid id)
        {
            Booking booking = Get(id);

            if (booking != null) {
                Context.Bookings.Remove(booking);
                Context.SaveChanges();
            }

            return booking;
        }
    }
}
