using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/lessors/{lessorId}/properties/{propertyId}/propertydetails/[controller]")]
    public class PropertyResourcesController : ControllerBase
    {
        private readonly IPropertyResourceService _propertyResourceService;
        private readonly IMapper _mapper;

        public PropertyResourcesController(IPropertyResourceService propertyResourceService, IMapper mapper)
        {
            _propertyResourceService = propertyResourceService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get all property resources",
            Description = "Get all property resources",
            OperationId = "GetAllPropertyResource",
            Tags = new[] { "property_resources" })]
        [HttpGet]
        public async Task<IEnumerable<PropertyResourceResource>> GetAllAsync(int lessorId, int propertyId)
        {
            var propertyResources = await _propertyResourceService.ListByPropertyDetailId(lessorId, propertyId);
            var resources = _mapper.Map<IEnumerable<Domain.Models.PropertyResource>, IEnumerable<PropertyResourceResource>>(propertyResources);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Create property resource",
            Description = "Create a new property resource of an specific property",
            OperationId = "CreatePropertyResource",
            Tags = new[] { "property_resources" })]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePropertyResourceResource resource, int lessorId, int propertyId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var propertyResource = _mapper.Map<SavePropertyResourceResource, Domain.Models.PropertyResource>(resource);

            var result = await _propertyResourceService.SaveAsync(lessorId, propertyId, propertyResource);

            if (!result.Success)
                return BadRequest(result.Message);

            var propertyResourceResource = _mapper.Map<Domain.Models.PropertyResource, PropertyResourceResource>(propertyResource);

            return Ok(propertyResourceResource);
        }
    }
}
