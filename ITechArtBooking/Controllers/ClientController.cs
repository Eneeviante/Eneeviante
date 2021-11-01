using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Services;
using ITechArtBooking.Domain.Models;
using ITechArtBooking.Infrastucture.Repositories.Fakes;

namespace ITechArtBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService postsService = new(new ClientsFakeRepository());

        [HttpGet]
        public List<Client> GetAll()
        {
            return postsService.GetAll();
        }
    }
}
