using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        //private readonly ClientService postsService = new(new ClientsFakeRepository());
        private readonly IReviewRepository reviewRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IClientRepository clientRepository;

        public ReviewController(IReviewRepository _reviewRepository,
            IHotelRepository _hotelRepository, IClientRepository _clientRepository)
        {
            reviewRepository = _reviewRepository;
            hotelRepository = _hotelRepository;
            clientRepository = _clientRepository;
        }

        [HttpGet(Name = "GetAllReviews")]
        public IEnumerable<Review> GetAll()
        {
            return reviewRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(long id)
        {
            Review review = reviewRepository.Get(id);

            if (review == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(review);
            }
        }

        [HttpPost]
        public IActionResult Create(long hotelId, int clientId, string text)
        {
            Review review = new Review {
                Id = 0,
                HotelId = hotelId,
                ClientId = clientId,
                Text = text
            };

            if(clientRepository.Get(clientId) == null || hotelRepository.Get(hotelId) == null) {
                return BadRequest();
            }

            reviewRepository.Create(review);
            return CreatedAtRoute("GetReview", new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, long hotelId, int clientId, string text)
        {
            Review newReview = new Review {
                Id = id,
                HotelId = hotelId,
                ClientId = clientId,
                Text = text
            };

            var oldReview = reviewRepository.Get(id);
            if (oldReview == null) {
                return NotFound();
            }

            if (clientRepository.Get(clientId) == null || hotelRepository.Get(hotelId) == null) {
                return BadRequest();
            }

            reviewRepository.Update(newReview);
            return RedirectToRoute("GetAllReviews");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deletedReview = reviewRepository.Delete(id);

            if (deletedReview == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedReview);
        }
    }
}
