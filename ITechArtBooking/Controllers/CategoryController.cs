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
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("{hotelId}")]
        public async Task<IEnumerable<Category>> GetAllAsync(Guid hotelId)
        {
            return await categoryService.GetAllAsync(hotelId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{hotelId}, {bedsNumber}, {description}, {costPerDay}")]
        public async Task<IActionResult> CreateAsync(Guid hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            var newCategory = await categoryService.CreateAsync(hotelId, bedsNumber,
                description, costPerDay);
            if(newCategory == null) {
                return BadRequest("Invalid hotel id");
            }
            return CreatedAtRoute(new { id = newCategory.Id }, newCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync(Guid categoryId)
        {
            var deletedCategory = await categoryService.DeleteAsync(categoryId);

            if (deletedCategory == null) {
                return BadRequest("Invalid category id");
            }

            return new ObjectResult(deletedCategory);
        }
    }
}
