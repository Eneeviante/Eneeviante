using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFClientDBContext : DbContext
    {
        public EFClientDBContext(DbContextOptions<EFClientDBContext> options) : base(options)
        { }
        public DbSet<Client> TodoItems { get; set; }
    }
}
