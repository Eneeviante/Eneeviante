using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITechArtBooking.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        //public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
    }
}