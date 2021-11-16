using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        public IEnumerable<Booking> GetAll();
        public Booking Get(Guid id);
        void Create(Booking category);
        void Update(Booking category);
        Booking Delete(Guid id);
    }
}
