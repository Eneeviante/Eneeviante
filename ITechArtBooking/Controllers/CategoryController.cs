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
    public class CategoryController : ControllerBase
    {
        //private readonly ClientService postsService = new(new ClientsFakeRepository());
        private readonly ICategoryRepository categoryRepository;
        private readonly IHotelRepository hotelRepository;

        public CategoryController(ICategoryRepository _categoryRepository, 
            IHotelRepository _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            hotelRepository = _hotelRepository;
        }

        [HttpGet(Name = "GetAllCategories")]
        public IEnumerable<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(long id)
        {
            Category category = categoryRepository.Get(id);

            if (category == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(category);
            }
        }

        [HttpPost]
        public IActionResult Create(long hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            Category category = new Category {
                Id = 0,
                HotelId = hotelId,
                BedsNumber = bedsNumber,
                CostPerDay = costPerDay,
                Description = description
            };

            Hotel hotel = hotelRepository.Get(hotelId);
            if (hotel == null) {
                return BadRequest();
            }

            categoryRepository.Create(category);
            hotel.Categories.Add(category);
            hotelRepository.Update(hotel);

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, long hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            Category newCategory = new Category {
                Id = id,
                HotelId = hotelId,
                BedsNumber = bedsNumber,
                Description = description,
                CostPerDay = costPerDay
            };

            var oldCategory = categoryRepository.Get(id);
            var oldHotel = hotelRepository.Get(oldCategory.HotelId);
            var newHotel = hotelRepository.Get(hotelId);
            if (oldCategory == null || newHotel == null) {
                return NotFound();
            }

            categoryRepository.Update(newCategory);
            if(newCategory.HotelId != oldCategory.HotelId) {
                oldHotel.Categories.Remove(oldCategory);
                hotelRepository.Update(oldHotel);
                newHotel.Categories.Add(newCategory);
                hotelRepository.Update(newHotel);
            }

            return RedirectToRoute("GetAllCategories");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deletedCategory = categoryRepository.Delete(id);

            if (deletedCategory == null) {
                return BadRequest();
            }

            var hotel = hotelRepository.Get(deletedCategory.HotelId);
            if(hotel == null) {
                return BadRequest();
            }

            hotel.Categories.Remove(deletedCategory);
            hotelRepository.Update(hotel);

            return new ObjectResult(deletedCategory);
        }
    }
}
