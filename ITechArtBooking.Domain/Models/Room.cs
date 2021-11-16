using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Picture { get; set; }
    }
}
