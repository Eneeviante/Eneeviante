using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;

namespace ITechArtBooking.Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly UserManager<User> userManager;

        public ReviewService(IReviewRepository _reviewRepository,
            IHotelRepository _hotelRepository, UserManager<User> _userManager)
        {
            reviewRepository = _reviewRepository;
            hotelRepository = _hotelRepository;
            userManager = _userManager;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(Guid hotelId)
        {
            return await reviewRepository.GetAllAsync(hotelId);
        }

        public async Task<Review> CreateAsync(Guid userId, Guid hotelId, string text)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var hotel = await hotelRepository.GetAsync(hotelId);

            if (user == null || hotel == null) {
                return null;
            }

            Review review = new Review {
                Id = new Guid(),
                Hotel = hotel,
                User = user,
                Text = text
            };

            await reviewRepository.CreateAsync(review);
            return review;
        }
    }
}
