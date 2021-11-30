using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public Hotel Hotel { get; set; }
        public int BedsNumber { get; set; }
        public string Description { get; set; }
        public float CostPerDay { get; set; }
    }
}
