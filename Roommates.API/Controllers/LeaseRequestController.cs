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
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/students/{studentId}/leaserequest")]
    [ApiController]
    public class LeaseRequestController : ControllerBase
    {
        private readonly IRequestService _friendshipRequestService;
        private readonly IMapper _mapper;

        public LeaseRequestController(IRequestService friendshipRequestService, IMapper mapper)
        {
            _friendshipRequestService = friendshipRequestService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Send lease request",
           Description = "Send a new lease request",
           OperationId = "SendLeaseRequest",
           Tags = new[] { "lease_requests" }
           )]
        [HttpPost("{lessorId}")]
        public async Task<IActionResult> SendLeaseRequest(int studentId, int lessorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var result = await _friendshipRequestService.AddLeaseRequestAsync(studentId, lessorId);

            var friendshipRequest = await _friendshipRequestService.GetByPersonOneIdAndPersonTwoId(studentId, lessorId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipRequestResource = _mapper.Map<Request, RequestResource>(friendshipRequest.Resource);

            return Ok(friendshipRequestResource);
        }

        [SwaggerOperation(
           Summary = "Get all sent lease request",
           Description = "Get all lease requests sent to a lessor",
           OperationId = "GetSentLeaseRequest",
           Tags = new[] { "lease_requests" }
           )]
        [HttpGet("sent")]
        public async Task<IEnumerable<RequestResource>> GetLeaseRequestsSent(int studentId)
        {
            var friendshipRequests = await _friendshipRequestService.GetSentRequests(studentId);
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(friendshipRequests);
            return resources;
        }

        [SwaggerOperation(
          Summary = "Delete Lease request",
          Description = "Delete Lease request",
          OperationId = "DeleteLeaseRequest",
          Tags = new[] { "lease_requests" }
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
