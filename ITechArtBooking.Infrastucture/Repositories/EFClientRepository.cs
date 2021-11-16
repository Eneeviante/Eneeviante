using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Interfaces;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Infrastucture.Repositories
{
    public class EFClientRepository : IClientRepository
    {
        private readonly EFBookingDBContext Context;

        public EFClientRepository(EFBookingDBContext context)
        {
            Context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return Context.Clients;
        }

        public Client Get(Guid id)
        {
            return Context.Clients.Find(id);
        }

        public void Create(Client client)
        {
            Context.Clients.Add(client);
            Context.SaveChanges();
        }

        public void Update(Client newClient)
        {
            Client currentClient = Get(newClient.Id);

            currentClient.LastName = newClient.LastName;
            currentClient.FirstName = newClient.FirstName;
            currentClient.MiddleName = newClient.MiddleName;
            currentClient.PhoneNumber = newClient.PhoneNumber;

            Context.Clients.Update(currentClient);
            Context.SaveChanges();
        }

        public Client Delete(Guid id)
        {
            Client client = Get(id);

            if(client != null) {
                Context.Clients.Remove(client);
                Context.SaveChanges();
            }

            return client;
        }
    }
}
