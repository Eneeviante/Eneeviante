using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StarNumber { get; set; }
        public string Description { get; set; }
    }
}
