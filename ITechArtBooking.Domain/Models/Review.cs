using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Review
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public Client Client { get; set; }
        public string text { get; set; }
    }
}
