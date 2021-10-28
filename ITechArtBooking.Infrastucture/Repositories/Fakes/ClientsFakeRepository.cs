using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories.Fakes
{
    public class ClientsFakeRepository : IClientRepository
    {
        public List<Client> GetAll()
        {
            return new List<Client>()
            {
                new Client
                {
                    Id = 1,
                    FirstName = "adawdaw",
                    MiddleName = "dawdwad",
                    LastName = "adawd",
                    PhoneNumber = "+3423423423"
                },
            }; ;
        }
    }
}
