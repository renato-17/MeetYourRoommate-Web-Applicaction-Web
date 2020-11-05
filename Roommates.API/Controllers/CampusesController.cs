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
    [Route("api/studyCenters/{studyCenterId}/[controller]")]
    [ApiController]
    public class CampusesController: ControllerBase
    {
        private readonly ICampusService _campusService;
        private readonly IMapper _mapper;

        public CampusesController(ICampusService campusService, IMapper mapper)
        {
            _campusService = campusService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CampusResource>> GetAllByStudyCenterId(int studyCenterId)
        {
            var categories = await _campusService.ListByStudyCenterIdAsync(studyCenterId);
            var resources = _mapper.Map<IEnumerable<Campus>, IEnumerable<CampusResource>>(categories);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<CampusResource> GetByIdAndStudyCenterId(int id, int studyCenterId)
        {
            var campus = await _campusService.GetByIdAndStudyCenterIdAsync(studyCenterId, id);
            var resource = _mapper.Map<Campus, CampusResource>(campus.Resource);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCampusResource resource, int studyCenterId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var campus = _mapper.Map<SaveCampusResource, Campus>(resource);
            var result = await _campusService.SaveAsync(studyCenterId,campus);

            if (!result.Success)
                return BadRequest(result.Message);

            var campusResource = _mapper.Map<Campus, CampusResource>(result.Resource);
            return Ok(campusResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCampusResource resource, int studyCenterId)
        {
            var campus = _mapper.Map<SaveCampusResource, Campus>(resource);
            var result = await _campusService.UpdateAsync(studyCenterId,id, campus);

            if (!result.Success)
                return BadRequest(result.Message);
            var campusResource = _mapper.Map<Campus, CampusResource>(result.Resource);
            return Ok(campusResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, int studyCenterId)
        {
            var result = await _campusService.DeleteAsync(studyCenterId,id);
            if (!result.Success)
                return BadRequest(result.Message);
            var campusResource = _mapper.Map<Campus, CampusResource>(result.Resource);
            return Ok(campusResource);

        }
    }
}
