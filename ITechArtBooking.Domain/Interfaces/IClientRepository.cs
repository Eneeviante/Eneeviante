using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IClientRepository
    {
        public IEnumerable<Client> GetAll();
        public Client Get(long id);
        void Create(string firstName, string middleName,
            string lastName, string Number);
        //void Create(Client client);
        void Update(long id, string firstName, string middleName,
            string lastName, string Number);
        Client Delete(long id);
    }
}
