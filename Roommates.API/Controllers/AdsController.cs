using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IMapper _mapper;

        public AdsController(IAdService adService, IMapper mapper)
        {
            _adService = adService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get all ads",
            Description = "Get all ads",
            OperationId = "GetAllAds",
            Tags = new[] { "ads" }
            )]
        [HttpGet]
        public async Task<IEnumerable<AdResource>> GetAllAsync()
        {
            var ads = await _adService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Ad>, IEnumerable<AdResource>>(ads);
            return resource;
        }
     
        [SwaggerOperation(
            Summary = "Get ad by Id",
            Description = "Get an specific Ad by its Id",
            OperationId = "GetAdById",
            Tags = new[] { "ads" }
            )]
        [HttpGet("{adId}")]
        public async Task<AdResource> GetByIdAsync(int adId)
        {
            var ad = await _adService.GetById(adId);
            var resource = _mapper.Map<Ad, AdResource>(ad.Resource);
            return resource;
        }

        [SwaggerOperation(
          Summary = "Create Ad",
          Description = "Create a new Ad",
          OperationId = "CreateAd",
          Tags = new[] { "ads" }
          )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAdResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var ad = _mapper.Map<SaveAdResource, Ad>(resource);
            var result = await _adService.SaveAsync(ad, resource.LessorId, resource.PropertyId);
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
        public async Task<IActionResult> PutAsync([FromBody] SaveAdResource resource, int id)
        {
            var ad = _mapper.Map<SaveAdResource, Ad>(resource);
            var result = await _adService.UpdateAsync(ad, id, resource.LessorId, resource.PropertyId);

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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _adService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studyCenterResource = _mapper.Map<Ad, AdResource>(result.Resource);

            return Ok(studyCenterResource);
        }

    }
}
