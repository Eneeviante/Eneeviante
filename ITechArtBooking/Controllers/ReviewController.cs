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
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }

        /*Посмтреть все отзывы об отеле*/
        [Authorize(Roles = "User")]
        [HttpGet("{hotelId}")]
        public async Task<IEnumerable<Review>> GetAllAsync(Guid hotelId)
        {
            return await reviewService.GetAllAsync(hotelId);
        }

        /*Оставлять отзыв о конкретном отеле*/
        [Authorize(Roles = "User")]
        [HttpPost("{hotelId}, {text}")]
        public async Task<IActionResult> CreateAsync(Guid hotelId, string text)
        {
            var userId = User.GetUserId();

            var newReview = await reviewService.CreateAsync(userId, hotelId, text);
            if (newReview == null) {
                return BadRequest("Invalid hotel id");
            }

            return CreatedAtRoute(new { id = newReview.Id }, newReview);
        }
    }
}
