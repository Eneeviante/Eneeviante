using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
    }
}
