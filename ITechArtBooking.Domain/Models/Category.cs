using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Category
    {
        public long Id { get; set; }
        public int BedsNumber { get; set; }
        public string Description { get; set; }
        public float CostPerDay { get; set; }
    }
}
