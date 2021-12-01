using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll(Guid userId);
        Room Get(Guid id);
        void Create(Room room);
        void Update(Room room);
        Room Delete(Guid id);
    }
}
