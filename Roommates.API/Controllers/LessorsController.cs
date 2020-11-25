using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LessorsController : ControllerBase
    {
        private readonly ILessorService _lessorService;
        private readonly IMapper _mapper;

        public LessorsController(ILessorService lessorService, IMapper mapper)
        {
            _lessorService = lessorService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all lessors",
            Description = "List all lessors",
            OperationId = "ListAllLessors",
            Tags = new[] { "lessors" }
            )]
        [HttpGet]
        public async Task<IEnumerable<LessorResource>> GetAllAsync()
        {
            var lessors = await _lessorService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Lessor>, IEnumerable<LessorResource>>(lessors);
            return resources;
        }

        [SwaggerOperation(
            Summary = "Get a Lessor",
            Description = "Get an specific lessor by id",
            OperationId = "GetLessorById",
            Tags = new[] { "lessors" }
            )]
        [HttpGet("{id}")]
        public async Task<LessorResource> GetByLessortId(int id)
        {
            var lessor = await _lessorService.GetByIdAsync(id);
            var resource = _mapper.Map<Lessor, LessorResource>(lessor.Resource);
            return resource;
        }

        [SwaggerOperation(
            Summary = "Create a lessor",
            Description = "Create a new Lessor",
            OperationId = "CreateLessor",
            Tags = new[] { "lessors" }
            )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveLessorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var lessor = _mapper.Map<SaveLessorResource, Lessor>(resource);

            var result = await _lessorService.SaveAsync(lessor);

            if (!result.Success)
                return BadRequest(result.Message);

            var lessorResource = _mapper.Map<Lessor, LessorResource>(result.Resource);

            return Ok(lessorResource);
        }

        [SwaggerOperation(
           Summary = "Delete a Lessor",
           Description = "Delete an specific Lessor",
           OperationId = "DeleteLessor",
           Tags = new[] { "lessors" }
           )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _lessorService.RemoveAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var lessorResource = _mapper.Map<Lessor, LessorResource>(result.Resource);
            return Ok(lessorResource);
        }

        [SwaggerOperation(
          Summary = "Update Lessor",
          Description = "Update an specific Lessor",
          OperationId = "UpdateLessor",
          Tags = new[] { "lessors" }
          )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveLessorResource resource, int id)
        {
            var lessor = _mapper.Map<SaveLessorResource, Lessor>(resource);
            var result = await _lessorService.UpdateAsync(id, lessor);

            if (!result.Success)
                return BadRequest(result.Message);

            var lessorResource = _mapper.Map<Lessor, LessorResource>(result.Resource);

            return Ok(lessorResource);
        }

        [SwaggerOperation(
           Summary = "Authenticate Lessor",
           Description = "Authenticate an specific Lessor",
           OperationId = "AuthenticateLessor",
           Tags = new[] { "lessors" }
           )]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _lessorService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Invalid Username or Password" });

            return Ok(response);
        }
    }
}
