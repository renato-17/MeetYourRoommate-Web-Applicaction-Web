﻿using System;
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
            Description = "List of comments by an specific Ad",
            OperationId = "ListCommentByAd",
            Tags = new[] { "comments" }
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
           Tags = new[] { "comments" }
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

        [SwaggerOperation(
           Summary = "Update comment",
           Description = "Update an specific comment",
           OperationId = "CreateComment",
           Tags = new[] { "comments" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveCommentResource resource, int id, int adId)
        {
            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _commentService.UpdateAsync(comment, id, adId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
         Summary = "Delete comment",
         Description = "Delete an specific comment",
         OperationId = "DeleteComment",
         Tags = new[] { "comments" }
         )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, int adId)
        {
            var result = await _commentService.DeleteAsync(id,adId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studyCenterResource = _mapper.Map<Comment, CommentResource>(result.Resource);

            return Ok(studyCenterResource);
        }
    }
}
