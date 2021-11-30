using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<Review> reviewRepository;
        private readonly IRepository<Hotel> hotelRepository;
        private readonly IRepository<Client> clientRepository;

        public ReviewController(IRepository<Review> _reviewRepository,
            IRepository<Hotel> _hotelRepository, IRepository<Client> _clientRepository)
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
        public IActionResult Get(Guid id)
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
        public IActionResult Create(Guid hotelId, Guid clientId, string text)
        {
            var client = clientRepository.Get(clientId);
            var hotel = hotelRepository.Get(hotelId);

            if (client == null || hotel == null) {
                return BadRequest();
            }

            Review review = new Review {
                Id = new Guid(),
                Hotel = hotel,
                Client = client,
                Text = text
            };

            reviewRepository.Create(review);
            return CreatedAtRoute("GetReview", new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Guid hotelId, Guid clientId, string text)
        {
            var oldReview = reviewRepository.Get(id);
            if (oldReview == null) {
                return NotFound();
            }

            var newClient = clientRepository.Get(clientId);
            var newHotel = hotelRepository.Get(hotelId);

            if (newClient == null || newHotel == null) {
                return BadRequest();
            }

            Review newReview = new Review {
                Id = id,
                Hotel = newHotel,
                Client = newClient,
                Text = text
            };

            reviewRepository.Update(newReview);
            return RedirectToRoute("GetAllReviews");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedReview = reviewRepository.Delete(id);

            if (deletedReview == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedReview);
        }
    }
}
