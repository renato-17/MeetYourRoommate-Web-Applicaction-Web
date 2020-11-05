using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Extensions;
using Roommates.API.Resource;

namespace Roommates.API.Controllers
{
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
        [HttpGet]
        public async Task<IEnumerable<LessorResource>> GetAllAsync()
        {
            var lessors = await _lessorService.LystAsync();
            var resources = _mapper.Map<IEnumerable<Lessor>, IEnumerable<LessorResource>>(lessors);
            return resources;
        }
        [HttpGet("{id}")]
        public async Task<LessorResource> GetByLessortId(int id)
        {
            var lessor = await _lessorService.GetByIdAsync(id);
            var resource = _mapper.Map<Lessor, LessorResource>(lessor.Resource);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(SaveLessorResource resource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var lessor = _mapper.Map<SaveLessorResource, Lessor>(resource);

            var result = await _lessorService.SaveAsync(lessor);

            if (!result.Success)
                return BadRequest(result.Message);

            var lessorResource = _mapper.Map<Lessor, LessorResource>(result.Resource);

            return Ok(lessorResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _lessorService.RemoveAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var lessorResource = _mapper.Map<Lessor, LessorResource>(result.Resource);
            return Ok(lessorResource);
        }
    }
}
