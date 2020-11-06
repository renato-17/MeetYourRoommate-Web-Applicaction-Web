using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading.Tasks;
using AutoMapper;
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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamsController(ITeamService TeamService, IMapper mapper)
        {
            _teamService = TeamService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all teams",
           Description = "List all teams",
           OperationId = "GetAllTeams",
           Tags = new[] { "teams" }
           )]
        [HttpGet]
        public async Task<IEnumerable<TeamResource>> GetAllAsync()
        {
            var teams = await _teamService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Team>, IEnumerable<TeamResource>>(teams);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get team",
           Description = "Get an specific team by its id",
           OperationId = "GetTeamById",
           Tags = new[] { "teams" }
           )]
        [HttpGet("{id}")]
        public async Task<TeamResource> GetByTeamId(int id)
        {
            var teams = await _teamService.GetByIdAsync(id);
            var resource = _mapper.Map<Team, TeamResource>(teams.Resource);
            return resource;
        }


        [SwaggerOperation(
           Summary = "Create team",
           Description = "Create a new Team",
           OperationId = "CreateTeam",
           Tags = new[] { "teams" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTeamResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var teams = _mapper.Map<SaveTeamResource, Team>(resource);

            var result = await _teamService.SaveAsync(teams);

            if (!result.Success)
                return BadRequest(result.Message);

            var teamResource = _mapper.Map<Team, TeamResource>(teams);

            return Ok(teamResource);
        }

        [SwaggerOperation(
           Summary = "Delete team",
           Description = "Delete an specific team",
           OperationId = "DeleteTeam",
           Tags = new[] { "teams" }
           )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _teamService.RemoveAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var teamResource = _mapper.Map<Team, TeamResource>(result.Resource);

            return Ok(teamResource);
        }
    }
}
