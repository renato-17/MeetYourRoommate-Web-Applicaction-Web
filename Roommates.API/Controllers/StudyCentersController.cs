using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudyCentersController: ControllerBase
    {
        private readonly IStudyCenterService _studyCenterService;
        private readonly IMapper _mapper;

        public StudyCentersController(IStudyCenterService studyCenterService, IMapper mapper)
        {
            _studyCenterService = studyCenterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudyCenterResource>> GetAllAsync()
        {
            var studyCenters = await _studyCenterService.ListAsync();
            var resource = _mapper.Map<IEnumerable<StudyCenter>, IEnumerable<StudyCenterResource>>(studyCenters);
            return resource;
        }

        [HttpGet("{id}")]
        public async Task<StudyCenterResource> GetByStudyCenterId(int id)
        {
            var studyCenters = await _studyCenterService.GetByIdAsync(id);
            var resource = _mapper.Map<StudyCenter, StudyCenterResource>(studyCenters.Resource);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveStudyCenterResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var studyCenters = _mapper.Map<SaveStudyCenterResource, StudyCenter>(resource);
            var result = await _studyCenterService.SaveAsync(studyCenters);

            if (!result.Success)
                return BadRequest(result.Message);

            var studyCenterResource = _mapper.Map<StudyCenter, StudyCenterResource>(studyCenters);
            return Ok(studyCenterResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _studyCenterService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studyCenterResource = _mapper.Map<StudyCenter, StudyCenterResource>(result.Resource);

            return Ok(studyCenterResource);
        }
    }
}

