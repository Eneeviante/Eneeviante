using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFBookingDBContext Context;

        public EFCategoryRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return Context.Categories;
        }

        public Category Get(long id)
        {
            return Context.Categories.Find(id);
        }

        public void Create(Category category)
        {
            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        public void Update(Category category)
        {
            Category currentCategory = Get(category.Id);

            currentCategory.HotelId = category.HotelId;
            currentCategory.BedsNumber = category.BedsNumber;
            currentCategory.Description = category.Description;
            currentCategory.CostPerDay = category.CostPerDay;
            
            Context.Categories.Update(currentCategory);
            Context.SaveChanges();
        }

        public Category Delete(long id)
        {
            Category category = Get(id);

            if (category != null) {
                Context.Categories.Remove(category);
                Context.SaveChanges();
            }

            return category;
        }
    }
}
