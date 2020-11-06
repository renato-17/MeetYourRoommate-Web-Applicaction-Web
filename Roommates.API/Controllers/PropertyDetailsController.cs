using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/lessors/{lessorId}/properties/{propertyId}/[controller]")]
    [ApiController]
    public class PropertyDetailsController : ControllerBase
    {
        private readonly IPropertyDetailService _propertyDetailService;
        private readonly IMapper _mapper;

        public PropertyDetailsController(IPropertyDetailService propertyDetailService, IMapper mapper)
        {
            _propertyDetailService = propertyDetailService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get Property Detail",
            Description = "Get the property detail of a especific property",
            OperationId = "GetPropertyDetail",
            Tags = new[] { "property_details" }
            )]
        [HttpGet]
        public async Task<PropertyDetailResource> GetPropertyDetailAsync(int lessorId, int propertyId)
        {
            var propertyDetail = await _propertyDetailService.GetPropertyDetailAsync(lessorId, propertyId);
            var resource = _mapper.Map<PropertyDetail, PropertyDetailResource>(propertyDetail.Resource);
            return resource;
        }

        [SwaggerOperation(
            Summary = "Create Property Detail",
            Description = "Create the Property detail of an specific property",
            OperationId = "CreatePropertyDetail",
            Tags = new[] { "property_details" }
            )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SavePropertyDetailResource resource, int lessorId, int propertyId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var propertyDetail = _mapper.Map<SavePropertyDetailResource, PropertyDetail>(resource);

            var result = await _propertyDetailService.SaveAsync(lessorId,propertyId,propertyDetail);

            if (!result.Success)
                return BadRequest(result.Message);

            var propertyDetailResource = _mapper.Map<PropertyDetail, PropertyDetailResource>(propertyDetail);

            return Ok(propertyDetailResource);
        }

    }
}
