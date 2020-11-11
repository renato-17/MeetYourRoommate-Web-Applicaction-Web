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
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/students/{studentId}/friendshiprequest")]
    [ApiController]
    public class FriendshipRequestsController : ControllerBase
    {
        private readonly IRequestService _friendshipRequestService;
        private readonly IMapper _mapper;

        public FriendshipRequestsController(IRequestService friendshipRequestService, IMapper mapper)
        {
            _friendshipRequestService = friendshipRequestService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Get all sent friendship request",
           Description = "Get all friendship requests sent to another student",
           OperationId = "GetSentLeaseRequest",
           Tags = new[] { "friendship_requests" }
           )]
        [HttpGet("sent")]
        public async Task<IEnumerable<RequestResource>> GetFriendshipRequestsSent(int studentId)
        {
            var friendshipRequests = await _friendshipRequestService.GetSentRequests(studentId);
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(friendshipRequests);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get all received friendship request",
           Description = "Get all friendship requests received from another student",
           OperationId = "GetReceivedLeaseRequest",
           Tags = new[] { "friendship_requests" }
           )]
        [HttpGet("received")]
        public async Task<IEnumerable<RequestResource>> GetFriendshipRequestsReceive(int studentId)
        {
            var friendshipRequests = await _friendshipRequestService.GetReceivedRequests(studentId);
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(friendshipRequests);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Send friendship request",
           Description = "Send a new friendship request",
           OperationId = "SendFriendshipRequest",
           Tags = new[] { "friendship_requests" }
           )]
        [HttpPost("{studentTwoId}")]
        public async Task<IActionResult> SendFriendshipRequest(int studentId, int studentTwoId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var result = await _friendshipRequestService.AddTeamRequestAsync(studentId, studentTwoId);

            var friendshipRequest = await _friendshipRequestService.GetByPersonOneIdAndPersonTwoId(studentId, studentTwoId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipRequestResource = _mapper.Map<Request, RequestResource>(friendshipRequest.Resource);

            return Ok(friendshipRequestResource);
        }

        [SwaggerOperation(
           Summary = "Answer friendship request",
           Description = "Answer friendship request",
           OperationId = "AnswerFriendshipRequest",
           Tags = new[] { "friendship_requests" }
           )]
        [HttpPut("{studentOneId}")]
        public async Task<IActionResult> AnswerRequest(int studentOneId, int studentId, [FromBody] RequestAnswerResource answerResource )
        {
            var result = await _friendshipRequestService.AnswerRequest(studentOneId, studentId, answerResource.Answer);

            if (!result.Success)
                return BadRequest(result.Message);

            var campusResource = _mapper.Map<Request, RequestResource>(result.Resource);
            return Ok(campusResource);
        }

        [SwaggerOperation(
           Summary = "Delete friendship request",
           Description = "Delete friendship request",
           OperationId = "DeleteFriendshipRequest",
           Tags = new[] { "friendship_requests" }
           )]
        [HttpDelete("{studentTwoId}")]
        public async Task<IActionResult> DeleteRequest(int studentTwoId, int studentId)
        {
            var result = await _friendshipRequestService.DeleteAsync(studentId, studentTwoId);

            if (!result.Success)
                return BadRequest(result.Message);

            var campusResource = _mapper.Map<Request, RequestResource>(result.Resource);
            return Ok(campusResource);
        }
    }
}
