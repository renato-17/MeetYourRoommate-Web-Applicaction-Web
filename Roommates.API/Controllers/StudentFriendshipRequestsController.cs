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
    [Route("api/students/{studentId}/friendshiprequest")]
    [ApiController]
    public class StudentFriendshipRequestsController : ControllerBase
    {
        private readonly IRequestService _friendshipRequestService;
        private readonly IMapper _mapper;

        public StudentFriendshipRequestsController(IRequestService friendshipRequestService, IMapper mapper)
        {
            _friendshipRequestService = friendshipRequestService;
            _mapper = mapper;
        }

        [HttpGet("sent")]
        public async Task<IEnumerable<FriendshipRequestResource>> GetFriendshipRequestsSent(int studentId)
        {
            var friendshipRequests = await _friendshipRequestService.GetFriendshipRequestSent(studentId);
            var resources = _mapper.Map<IEnumerable<FriendshipRequest>, IEnumerable<FriendshipRequestResource>>(friendshipRequests);
            return resources;
        }

        [HttpGet("receive")]
        public async Task<IEnumerable<FriendshipRequestResource>> GetFriendshipRequestsReceive(int studentId)
        {
            var friendshipRequests = await _friendshipRequestService.GetFriendshipRequestReceive(studentId);
            var resources = _mapper.Map<IEnumerable<FriendshipRequest>, IEnumerable<FriendshipRequestResource>>(friendshipRequests);
            return resources;
        }

        [HttpPost("{studentTwoId}")]
        public async Task<IActionResult> SendFriendshipRequest(int studentId, int studentTwoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var result = await _friendshipRequestService.AddTeamRequestAsync(studentId, studentTwoId);

            var friendshipRequest = await _friendshipRequestService.GetByPersonOneIdAndPersonTwoId(studentId, studentTwoId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipRequestResource = _mapper.Map<FriendshipRequest, FriendshipRequestResource>(friendshipRequest.Resource);

            return Ok(friendshipRequestResource);
        }

        [HttpPut("{studentOneId}")]
        public async Task<IActionResult> AnswerRequest(int studentOneId, int studentId, [FromBody] FriendshipAnswerResource answerResource )
        {
            var result = await _friendshipRequestService.AnswerRequest(studentOneId, studentId, answerResource.Answer);

            if (!result.Success)
                return BadRequest(result.Message);

            var campusResource = _mapper.Map<FriendshipRequest, FriendshipRequestResource>(result.Resource);
            return Ok(campusResource);
        }
    }
}
