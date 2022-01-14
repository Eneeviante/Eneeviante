using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Domain.Services.ServiceInterfaces;

namespace ITechArtBooking.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IHotelRepository hotelRepository;

        public CategoryService(ICategoryRepository _categoryRepository,
            IHotelRepository _hotelRepository)
        {
            categoryRepository = _categoryRepository;
            hotelRepository = _hotelRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid hotelId)
        {
            return await categoryRepository.GetAllAsync(hotelId);
        }

        public async Task<Category> CreateAsync(Guid hotelId, int bedsNumber,
            string description, float costPerDay)
        {
            var hotel = await hotelRepository.GetAsync(hotelId);
            if (hotel == null) {
                return null;
            }

            Category newCategory = new Category {
                Id = new Guid(),
                Hotel = hotel,
                BedsNumber = bedsNumber,
                CostPerDay = costPerDay,
                Description = description
            };

            await categoryRepository.CreateAsync(newCategory);
            return newCategory;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            return await categoryRepository.DeleteAsync(id);
        }
    }
}
