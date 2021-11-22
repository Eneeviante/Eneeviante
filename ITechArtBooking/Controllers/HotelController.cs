using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        //private readonly ClientService postsService = new(new ClientsFakeRepository());
        private readonly IRepository<Hotel> hotelRepository;

        public HotelController(IRepository<Hotel> _hotelRepository)
        {
            hotelRepository = _hotelRepository;
        }

        [HttpGet(Name = "GetAllHotels")]
        public IEnumerable<Hotel> GetAll()
        {
            return hotelRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetHotel")]
        public IActionResult Get(Guid id)
        {
            Hotel hotel = hotelRepository.Get(id);

            if (hotel == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(hotel);
            }
        }

        [HttpPost]
        public IActionResult Create(string name, string description, int starNumber)
        {
            Hotel hotel = new Hotel { 
                Id = new Guid(),
                Name = name,
                Description = description,
                StarNumber = starNumber
            };

            hotelRepository.Create(hotel);
            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, string name, string description, int starNumber)
        {
            var oldHotel = hotelRepository.Get(id);
            if (oldHotel == null) {
                return NotFound();
            }

            var newHotel = new Hotel {
                Id = id,
                Name = name,
                Description = description,
                StarNumber = starNumber
            };


            hotelRepository.Update(newHotel);
            return RedirectToRoute("GetAllHotels");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedHotel = hotelRepository.Delete(id);

            if (deletedHotel == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedHotel);
        }
    }
}
