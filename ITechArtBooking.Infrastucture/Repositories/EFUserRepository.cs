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

        public async Task<IQueryable> GetAllAsync()
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

        public async Task<User> GetAsync(Guid id)
        {
            return await Context.Users.FindAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User newUser)
        {
            User currentUser = await GetAsync(newUser.Id);

            currentUser.LastName = newUser.LastName;
            currentUser.FirstName = newUser.FirstName;
            currentUser.MiddleName = newUser.MiddleName;
            currentUser.PhoneNumber = newUser.PhoneNumber;

            Context.Users.Update(currentUser);
            await Context.SaveChangesAsync();
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            User user = await GetAsync(id);

            if(user != null) {
                Context.Users.Remove(user);
                await Context.SaveChangesAsync();
            }

            return user;
        }
    }
}
