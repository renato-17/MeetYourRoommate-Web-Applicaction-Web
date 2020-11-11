using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PeopleController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        [SwaggerOperation(
            Summary = "Confirm person's data",
            Description = "Confirm that mail and password of the person are correct",
            OperationId = "GetPersonByData",
            Tags = new[] { "people" }
            )]
        [HttpGet]
        public async Task<PersonResource> GetPersonByData([FromQuery]string mail, [FromQuery]string password)
        {
            var person = await _personService.GetPersonByDataAsync(mail, password);
            var resource = _mapper.Map<Person, PersonResource>(person.Resource);
            return resource;
        }

    }
}
