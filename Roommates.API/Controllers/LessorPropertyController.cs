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
           Tags = new[] { "properties" }
           )]
        [HttpGet]
        public async Task<IEnumerable<Resource.PropertyResource>> GetAllByLessorIdAsync(int lessorId)
        {
            var properties = await _propertyService.ListByLessorIdAsync(lessorId);
            var resource = _mapper.Map<IEnumerable<Property>, IEnumerable<Resource.PropertyResource>>(properties);
            return resource;
        }

        [SwaggerOperation(
        Summary = "Get property by its id an lessor",
        Description = "Get an specifi property by its id and Lessor id",
        OperationId = "GetPropertyByIdAndLessorId",
        Tags = new[] { "properties" }
        )]
        [HttpGet("{id}")]
        public async Task<Resource.PropertyResource> GetByPropertyIdAndLessorId(int id, int lessorId)
        {
            var property = await _propertyService.GetByIdAndLessorIdAsync(lessorId, id);
            var resource = _mapper.Map<Property, Resource.PropertyResource>(property.Resource);
            return resource;
        }


    }
}
