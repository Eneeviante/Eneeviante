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
        private readonly ICategoryRepository categoryRepository;
        private readonly IHotelRepository hotelRepository;

        public CategoryController(ICategoryRepository _categoryRepository,
            IHotelRepository _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            hotelRepository = _hotelRepository;
        }

        [Authorize(Roles = "User")]
        [HttpGet(Name = "GetAllCategoriesFromHotel")]
        public async Task<IEnumerable<Category>> GetAllAsync(Guid id)
        {
            return await categoryRepository.GetAllAsync(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            var hotel = await hotelRepository.GetAsync(hotelId);
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

            await categoryRepository.CreateAsync(newCategory);
            return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, newCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(id);

            if (deletedCategory == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedCategory);
        }
    }
}
