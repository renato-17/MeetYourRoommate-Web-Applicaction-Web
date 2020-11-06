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
    [Route("api/lessors/{lessorId}/leaserequest")]
    [ApiController]
    public class LessorLeaseRequestController : ControllerBase
    {
        private readonly IRequestService _friendshipRequestService;
        private readonly IMapper _mapper;

        public LessorLeaseRequestController(IRequestService friendshipRequestService, IMapper mapper)
        {
            _friendshipRequestService = friendshipRequestService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Get all received lease request",
           Description = "Get all lease requests received from a student",
           OperationId = "GetReceivedLeaseRequest",
           Tags = new[] { "lease_requests" }
           )]
        [HttpGet("receive")]
        public async Task<IEnumerable<RequestResource>> GetRequestsReceive(int lessorId)
        {
            var requests = await _friendshipRequestService.GetReceivedRequests(lessorId);
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Answer lease request",
           Description = "Answer lease request",
           OperationId = "AnswerLeaseRequest",
           Tags = new[] { "lease_requests" }
           )]
        [HttpPut("{studentOneId}")]
        public async Task<IActionResult> AnswerRequest(int studentOneId, int lessorId, [FromBody] RequestAnswerResource answerResource)
        {
            var result = await _friendshipRequestService.AnswerRequest(studentOneId, lessorId, answerResource.Answer);

            if (!result.Success)
                return BadRequest(result.Message);

            var campusResource = _mapper.Map<Request, RequestResource>(result.Resource);
            return Ok(campusResource);
        }
    }
}
