
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertiesController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Get all properties",
           Description = "Get all properties",
           OperationId = "GetAllProperties",
           Tags = new[] { "properties" }
           )]
        [HttpGet]
        public async Task<IEnumerable<Resource.PropertyResource>> GetAllAsync()
        {
            var properties = await _propertyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Property>, IEnumerable<Resource.PropertyResource>>(properties);
            return resources;
        }

    }
}
