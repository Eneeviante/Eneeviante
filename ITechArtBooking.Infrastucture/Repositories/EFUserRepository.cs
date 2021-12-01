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
    public class EFUserRepository : IUserRepository
    {
        private readonly EFBookingDBContext Context;

        public EFUserRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IQueryable GetAll()
        {
            return Context.Users
                .Join(Context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Category),
                user => user.Id,
                booking => booking.User.Id,
                (user, booking) => new {
                    user,
                    booking.Id,
                    booking.Room,
                    booking.DateFrom,
                    booking.DateTo,
                    booking.Sum
                });
        }

        public User Get(Guid id)
        {
            return Context.Users.Find(id);
        }

        public void Create(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Update(User newUser)
        {
            User currentUser = Get(newUser.Id);

            currentUser.LastName = newUser.LastName;
            currentUser.FirstName = newUser.FirstName;
            currentUser.MiddleName = newUser.MiddleName;
            currentUser.PhoneNumber = newUser.PhoneNumber;

            Context.Users.Update(currentUser);
            Context.SaveChanges();
        }

        public User Delete(Guid id)
        {
            User user = Get(id);

            if(user != null) {
                Context.Users.Remove(user);
                Context.SaveChanges();
            }

            return user;
        }
    }
}
