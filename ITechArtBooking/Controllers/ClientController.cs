using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Services;
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
        private readonly IClientRepository clientRepository;

        public ClientController(IClientRepository _clientRepository)
        {
            clientRepository = _clientRepository;
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<Client> GetAll()
        {
            return clientRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(long id)
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
        public IActionResult Create([FromBody] Client client)
        {
            if (client == null) {
                return BadRequest();
            }
            else {
                clientRepository.Create(client);
                return CreatedAtRoute("GetItem", new { id = client.Id }, client);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Client updatedClient)
        {
            if (updatedClient == null || updatedClient.Id != id) {
                return BadRequest();
            }

            var client = clientRepository.Get(id);
            if (client == null) {
                return NotFound();
            }

            clientRepository.Update(updatedClient);
            return RedirectToRoute("GetAllItems");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var deletedClient = clientRepository.Delete(id);

            if (deletedClient == null) {
                return BadRequest();
            }

            return new ObjectResult(deletedClient);
        }
    }
}
