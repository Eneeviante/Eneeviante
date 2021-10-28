using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Hotel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StarNumber { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Category> Categories { get; set; }
    }
}
