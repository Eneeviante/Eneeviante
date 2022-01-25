using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Models.Pagination;
using ITechArtBooking.Domain.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ITechArtBooking.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Register(string email, string firstName,
            string middleName, string lastName, string password)
        {
            var user = new User {
                Email = email,
                UserName = email,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) {
                return null;
            }
            await _userManager.AddToRoleAsync(user, "User");

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, "Registration", "You have successfully registered!");

            return user;
        }

        public async Task<dynamic> Login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) {
                return null;
            }
            if (!await _userManager.CheckPasswordAsync(user, password)) {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "")
            };


            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("aksdokjafbkjasbfjabojsfbda"));

            var token = new JwtSecurityToken(
                issuer: "test",
                audience: "test",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey,
                        SecurityAlgorithms.HmacSha256)
                );

            var result = new {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiration = token.ValidTo
            };

            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageSize, int pageNumber)
        {
            var users = _userManager.Users.ToList();

            PageModel<User> pageModel = new PageModel<User>(users, pageNumber, pageSize);
            
            if (!pageModel.IsCorrectPage()) {
                return null;
            }

            return pageModel.ItemsOnPage();
        }

        public async Task<User> DeleteAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.DeleteAsync(user);

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "Delete account", "Your account has been successfully deleted!");

            return user;
        }
    }
}
