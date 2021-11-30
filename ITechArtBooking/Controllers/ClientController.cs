using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
//using ITechArtBooking.Infrastucture.Repositories.Fakes;
using ITechArtBooking.Domain.Interfaces;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        //private readonly ClientService postsService = new(new ClientsFakeRepository());
        private readonly IRepository<Client> clientRepository;

        public ClientController(IRepository<Client> _clientRepository)
        {
            clientRepository = _clientRepository;
        }

        [HttpGet(Name = "GetAllClients")]
        public IEnumerable<Client> GetAll()
        {
            return clientRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult Get(Guid id)
        {
            Client client = clientRepository.Get(id);

            if(client == null) {
                return NotFound();
            }
            else {
                return new ObjectResult(client);
            }
        }

        [HttpPost]
        public IActionResult Create(string firstName, string middleName,
            string lastName, string phoneNumber)
        {
            Client client = new Client { 
                Id = new Guid(),
                FirstName = firstName,
                MiddleName =middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            if (client == null) {
                return BadRequest();
            }
            else {
                clientRepository.Create(client);
                return CreatedAtRoute("GetClient", new { id = client.Id }, client);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, string firstName, string middleName,
            string lastName, string phoneNumber)
        {
            var client = clientRepository.Get(id);
            if (client == null) {
                return NotFound();
            }

            var newClient = new Client {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            clientRepository.Update(newClient);
            return RedirectToRoute("GetAllClients");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deletedClient = clientRepository.Delete(id);

            if (deletedClient == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedClient);
        }
    }
}
