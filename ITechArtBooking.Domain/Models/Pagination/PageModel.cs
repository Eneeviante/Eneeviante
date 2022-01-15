using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models.Pagination
{
    public class PageModel<T>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; }

        public PageModel(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;

            int count = Items.Count();
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        
        public bool IsCorrectPage()
        {
            return PageNumber >= 1 && PageNumber <= TotalPages;
        }

        public IEnumerable<T> ItemsOnPage()
        {
            return Items
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
