using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LessorAdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IMapper _mapper;

        public LessorAdController(IAdService adService, IMapper mapper)
        {
            _adService = adService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List ads by lessor",
           Description = "List of ads for an specific lessor",
           OperationId = "ListAdByLessor",
           Tags = new[] { "ads" }
           )]
        [HttpGet("{lessorId}/ads")]
        public async Task<IEnumerable<AdResource>> GetAllAdsByLessorIdAsync(int lessorId)
        {
            var ads = await _adService.ListByLessorIdAsync(lessorId);
            var resources = _mapper.Map<IEnumerable<Ad>, IEnumerable<AdResource>>(ads);
            return resources;
        }
    }
}
