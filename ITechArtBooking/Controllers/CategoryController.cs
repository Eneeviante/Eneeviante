using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly UserService postsService = new(new UsersFakeRepository());
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Hotel> hotelRepository;

        public CategoryController(IRepository<Category> _categoryRepository,
            IRepository<Hotel> _hotelRepository)
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
        public IActionResult Get(Guid id)
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
        public IActionResult Create(Guid hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            var hotel = hotelRepository.Get(hotelId);
            if (hotel == null) {
                return BadRequest();
            }

            Category newCategory = new Category {
                Id = new Guid(),
                Hotel = hotel,
                BedsNumber = bedsNumber,
                CostPerDay = costPerDay,
                Description = description
            };

            categoryRepository.Create(newCategory);
            return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Guid hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            var newHotel = hotelRepository.Get(hotelId);
            if (newHotel == null) {
                return BadRequest();
            }

            var oldCategory = categoryRepository.Get(id);
            if (oldCategory == null) {
                return NotFound();
            }

            Category newCategory = new Category {
                Id = id,
                Hotel = newHotel,
                BedsNumber = bedsNumber,
                Description = description,
                CostPerDay = costPerDay
            };

            categoryRepository.Update(newCategory);
            return RedirectToRoute("GetAllCategories");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedCategory = categoryRepository.Delete(id);

            if (deletedCategory == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedCategory);
        }
    }
}
