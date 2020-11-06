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
    [Route("api/lessors/{lessorId}/properties/{propertyId}/ads")]
    [ApiController]
    public class PropertyAdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IMapper _mapper;

        public PropertyAdController(IAdService adService, IMapper mapper)
        {
            _adService = adService;
            _mapper = mapper;
        }
        [SwaggerOperation(
           Summary = "List ads by property",
           Description = "List of ads for an specific property",
           OperationId = "ListAdByProperty",
           Tags = new[] { "Properties" }
           )]
        [HttpGet]
        public async Task<IEnumerable<AdResource>> GetAllByLessorIdAndPropertyIdAsync(int lessorId, int propertyId)
        {
            var ads = await _adService.ListByLessorIdAndPropertyIdAsync(lessorId, propertyId);
            var resources = _mapper.Map<IEnumerable<Ad>, IEnumerable<AdResource>>(ads);
            return resources;
            //implementar getallbylessorId
        }
        
        [SwaggerOperation(
           Summary = "New ad",
           Description = "Create a new Ad",
           OperationId = "CreateAd",
           Tags = new[] { "Ads" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAdResource resource, int lessorId, int propertyId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var ad = _mapper.Map<SaveAdResource, Ad>(resource);
            var result = await _adService.SaveAsync(ad, lessorId, propertyId);
            if (!result.Success)
                return BadRequest(result.Message);
            var adResource = _mapper.Map<Ad, AdResource>(ad);
            return Ok(adResource);
        }
       
    }
}
