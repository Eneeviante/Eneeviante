using System.Collections.Generic;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
        public Category Get(long id);
        void Create(Category category);
        void Update(Category category);
        Category Delete(long id);
    }
}
