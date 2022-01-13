using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ITechArtBooking.Helper;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        //private readonly UserService postsService = new(new UsersFakeRepository());
        private readonly IReviewRepository reviewRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IUserRepository userRepository;

        public ReviewController(IReviewRepository _reviewRepository,
            IHotelRepository _hotelRepository, IUserRepository _userRepository)
        {
            reviewRepository = _reviewRepository;
            hotelRepository = _hotelRepository;
            userRepository = _userRepository;
        }

        [Authorize(Roles = "User")]
        [HttpGet(Name = "GetAllReviewsAboutHotel")]
        public async Task<IEnumerable<Review>> GetAllAsync(Guid hotelId)
        {
            return await reviewRepository.GetAllAsync(hotelId);
        }

        [HttpGet("{id}", Name = "GetReview")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            Review review = await reviewRepository.GetAsync(id);

            if (review == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(review);
            }
        }

        /*Оставлять отзыв о конкретном отеле*/
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid hotelId, string text)
        {
            var userId = this.User.GetUserId();

            var user = await userRepository.GetAsync(userId);
            var hotel = await hotelRepository.GetAsync(hotelId);

            if (user == null || hotel == null) {
                return BadRequest();
            }

            Review review = new Review {
                Id = new Guid(),
                Hotel = hotel,
                User = user,
                Text = text
            };

            await reviewRepository.CreateAsync(review);
            return CreatedAtRoute("GetReview", new { id = review.Id }, review);
        }
    }
}
