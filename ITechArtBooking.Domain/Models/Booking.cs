using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
        public float Sum { get; set; }
    }
}
