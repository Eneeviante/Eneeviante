using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITechArtBooking.Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public short Number { get; set; }
        [JsonIgnore]
        public Booking LastBooking { get; set; }
        public Category Category { get; set; }
        public string Picture { get; set; }
    }
}
