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
    [Route("api/[controller]/")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<CommentResource>> GetAllAsync()
        {
            var commets = await _commentService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(commets);
            return resource;
        }

        [SwaggerOperation(
           Summary = "comment by id",
           Description = "comment for an specific id",
           OperationId = "CommentById",
           Tags = new[] { "Comments" }
           )]
        [HttpGet("{commentId}")]
        public async Task<CommentResource> GetByIdAsync(int commentId)
        {
            var comment = await _commentService.FindById(commentId);
            var resource = _mapper.Map<Comment, CommentResource>(comment.Resource);
            return resource;
        }
    }
}
