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
           Tags = new[] { "ads" }
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
           Summary = "Create Ad",
           Description = "Create a new Ad",
           OperationId = "CreateAd",
           Tags = new[] { "ads" }
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

        [SwaggerOperation(
            Summary = "Update Ad",
            Description = "Update an specific Ad",
            OperationId = "CreateAd",
            Tags = new[] { "ads" }
            )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveAdResource resource, int id, int lessorId, int propertyId)
        {
            var ad = _mapper.Map<SaveAdResource, Ad>(resource);
            var result = await _adService.UpdateAsync(ad, id,lessorId,propertyId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Ad, AdResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
            Summary = "Delete ad",
            Description = "Delete an specific ad",
            OperationId = "DeleteAd",
            Tags = new[] { "ads" }
            )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, int lessorId, int propertyId)
        {
            var result = await _adService.DeleteAsync(id, lessorId,propertyId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studyCenterResource = _mapper.Map<Ad, AdResource>(result.Resource);

            return Ok(studyCenterResource);
        }
    }
}
