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
        private readonly IHotelRepository hotelRepository;

        public HotelController(IHotelRepository _hotelRepository)
        {
            hotelRepository = _hotelRepository;
        }

        [HttpGet(Name = "GetAllHotels")]
        public IEnumerable<Hotel> GetAll()
        {
            return hotelRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetHotel")]
        public IActionResult Get(long id)
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
        public IActionResult Create([FromBody] Hotel hotel)
        {
            if (hotel == null) {
                return BadRequest();
            }
            else {
                hotelRepository.Create(hotel);
                return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Hotel updatedHotel)
        {
            if (updatedHotel == null || updatedHotel.Id != id) {
                return BadRequest();
            }

            var hotel = hotelRepository.Get(id);
            if (hotel == null) {
                return NotFound();
            }

            hotelRepository.Update(updatedHotel);
            return RedirectToRoute("GetAllHotels");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deletedHotel = hotelRepository.Delete(id);

            if (deletedHotel == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedHotel);
        }
    }
}
