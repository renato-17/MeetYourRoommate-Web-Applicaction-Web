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

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/students/{studentId}/lessorrequest")]
    [ApiController]
    public class LessorRequestController : ControllerBase
    {
        private readonly IRequestService _friendshipRequestService;
        private readonly IMapper _mapper;

        public LessorRequestController(IRequestService friendshipRequestService, IMapper mapper)
        {
            _friendshipRequestService = friendshipRequestService;
            _mapper = mapper;
        }

        [HttpPost("{lessorId}")]
        public async Task<IActionResult> SendLessorRequest(int studentId, int lessorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var result = await _friendshipRequestService.AddLessorRequestAsync(studentId, lessorId);

            var friendshipRequest = await _friendshipRequestService.GetByPersonOneIdAndPersonTwoId(studentId, lessorId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipRequestResource = _mapper.Map<FriendshipRequest, FriendshipRequestResource>(friendshipRequest.Resource);

            return Ok(friendshipRequestResource);
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
    }
}
