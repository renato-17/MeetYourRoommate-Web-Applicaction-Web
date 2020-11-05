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
    [Route("api/ads/{adId}/comments")]
    [ApiController]
    public class AdCommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public AdCommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [SwaggerOperation(
            Summary = "List comments by ad",
            Description = "List of comments for an specific Ad",
            OperationId = "ListCommentByAd",
            Tags = new[] { "Ads" }
            )]
        [HttpGet]
        public async Task<IEnumerable<CommentResource>> GetAllByAdIdAsync(int adId)
        {
            var comments = await _commentService.ListByAdIdAsync(adId);
            var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Create  a comment",
           Description = "Create a new comment",
           OperationId = "CreateComment",
           Tags = new[] { "Comments" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource, int adId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());
            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _commentService.SaveAsync(comment, adId);
            if (!result.Success)
                return BadRequest(result.Message);
            var commentResource = _mapper.Map<Comment, CommentResource>(comment);
            return Ok(commentResource);
        }

    }
}
