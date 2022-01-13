using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories.Fakes
{
    public class SeedFirstData
    {
        public static async void SeedAdminUser(IServiceProvider serviceProvider)
        {
            var Context = (EFBookingDBContext) serviceProvider
                .GetService(typeof(EFBookingDBContext));
            var _userManager = (UserManager<User>) serviceProvider
                .GetService(typeof(UserManager<User>));
            var _roleManager = (RoleManager<IdentityRole<Guid>>)serviceProvider
                .GetService(typeof(RoleManager<IdentityRole<Guid>>));

            var user = new User {
                Email = "Admin@admin.com",
                UserName = "Admin@admin.com",
                FirstName = "Admin",
                MiddleName = "Admin",
                LastName = "Admin"
            };

            if (!Context.Roles.Any(r => r.Name == "Admin")) {
                _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"))
                    .GetAwaiter().GetResult();
            }

            if (!Context.Roles.Any(r => r.Name == "User")) {
                _roleManager.CreateAsync(new IdentityRole<Guid>("User"))
                    .GetAwaiter().GetResult();
            }

            if (!Context.Users.Any(u => u.UserName == user.UserName)) {
                _userManager.CreateAsync(user, "Passw0rd!")
                    .GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Admin")
                    .GetAwaiter().GetResult();
            }
        }
    }
}
