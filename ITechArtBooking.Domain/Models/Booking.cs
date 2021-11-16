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

        //[DataType(DataType.Time)]
        public DateTime DateFrom { get; set; }

        //[DataType(DataType.Time)]
        public DateTime DateTo { get; set; }
        public Room Room { get; set; }
        public Client Client { get; set; }
        public float Sum { get; set; }
    }
}
