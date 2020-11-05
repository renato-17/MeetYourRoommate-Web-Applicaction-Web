using System;
using System.Collections.Generic;
using System.Linq;
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
    [ApiController]
    [Produces("application/json")]
    [Route("/api/lessors/{lessorId}/properties")]
    public class LessorPropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public LessorPropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List properties by lessor",
            Description = "List of Properties for an specific Lessor",
            OperationId = "ListProperyByLessor",
            Tags = new[] { "Lessors" }
            )]
        [HttpGet]
        public async Task<IEnumerable<Resource.PropertyResource>> GetAllByLessorIdAsync(int lessorId)
        {
            var properties = await _propertyService.ListByLessorIdAsync(lessorId);
            var resource = _mapper.Map<IEnumerable<Property>, IEnumerable<Resource.PropertyResource>>(properties);
            return resource;
        }

        [HttpGet("{id}")]
        public async Task<Resource.PropertyResource> GetByPropertyIdAndLessorId(int id, int lessorId)
        {
            var property = await _propertyService.GetByIdAndLessorIdAsync(lessorId,id);
            var resource = _mapper.Map<Property, Resource.PropertyResource>(property.Resource);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(SavePropertyResource resource, int lessorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var property = _mapper.Map<SavePropertyResource, Property>(resource);

            var result = await _propertyService.SaveAsync(lessorId,property);

            if (!result.Success)
                return BadRequest(result.Message);

            var propertyResource = _mapper.Map<Property, Resource.PropertyResource>(result.Resource);

            return Ok(propertyResource);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, int lessorId)
        {
            var result = await _propertyService.RemoveAsync(lessorId,id);
            if (!result.Success)
                return BadRequest(result.Message);
            var propertyResource = _mapper.Map<Property, Resource.PropertyResource>(result.Resource);
            return Ok(propertyResource);
        }
    }
}
