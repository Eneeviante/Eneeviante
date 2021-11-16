using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IHotelRepository
    {
        public IEnumerable<Hotel> GetAll();
        public Hotel Get(Guid id);
        void Create(Hotel hotel);
        void Update(Hotel newHotel);
        Hotel Delete(Guid id);
    }
}
