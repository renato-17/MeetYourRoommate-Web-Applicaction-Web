
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
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

        [SwaggerOperation(
        Summary = "Get property by id",
        Description = "Get an specifi property by its id",
        OperationId = "GetPropertyById",
        Tags = new[] { "properties" }
        )]
        [HttpGet("{id}")]
        public async Task<Resource.PropertyResource> GetById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            var resource = _mapper.Map<Property, Resource.PropertyResource>(property.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create a property",
           Description = "Create a new property",
           OperationId = "CreateProperty",
           Tags = new[] { "properties" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePropertyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var property = _mapper.Map<SavePropertyResource, Property>(resource);

            var result = await _propertyService.SaveAsync(resource.LessorId, property);

            if (!result.Success)
                return BadRequest(result.Message);

            var propertyResource = _mapper.Map<Property, Resource.PropertyResource>(result.Resource);

            return Ok(propertyResource);
        }

        [SwaggerOperation(
         Summary = "Update Property",
         Description = "Update an specific Property",
         OperationId = "UpdateProperty",
         Tags = new[] { "properties" }
         )]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SavePropertyResource resource, int id)
        {
            var property = _mapper.Map<SavePropertyResource, Property>(resource);

            var result = await _propertyService.UpdateAsync(resource.LessorId, id, property);
            if (!result.Success)
                return BadRequest(result.Message);
            var propertyResource = _mapper.Map<Property, Resource.PropertyResource>(result.Resource);
            return Ok(propertyResource);
        }

        [SwaggerOperation(
           Summary = "Delete Property",
           Description = "Delete an specific Property",
           OperationId = "DeleteProperty",
           Tags = new[] { "properties" }
           )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _propertyService.RemoveAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var propertyResource = _mapper.Map<Property, Resource.PropertyResource>(result.Resource);
            return Ok(propertyResource);
        }
    }
}
