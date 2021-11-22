using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFCategoryRepository : IRepository<Category>
    {
        private readonly EFBookingDBContext Context;

        public EFCategoryRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return Context.Categories
                .Include(c => c.Hotel)
                .ToList();
        }

        public Category Get(Guid id)
        {
            return Context.Categories
                .Include(c => c.Hotel)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Create(Category category)
        {
            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        public void Update(Category category)
        {
            Category currentCategory = Get(category.Id);

            currentCategory.Hotel = category.Hotel;
            currentCategory.BedsNumber = category.BedsNumber;
            currentCategory.Description = category.Description;
            currentCategory.CostPerDay = category.CostPerDay;
            
            Context.Categories.Update(currentCategory);
            Context.SaveChanges();
        }

        public Category Delete(Guid id)
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
