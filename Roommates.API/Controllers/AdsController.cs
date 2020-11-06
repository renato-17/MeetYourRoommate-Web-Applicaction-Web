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
    [Route("api/[controller]/")]
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


    }
}
