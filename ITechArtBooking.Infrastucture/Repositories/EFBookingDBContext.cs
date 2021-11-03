using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFBookingDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public EFBookingDBContext(DbContextOptions<EFBookingDBContext> options) : base(options)
        { }
    }
}
