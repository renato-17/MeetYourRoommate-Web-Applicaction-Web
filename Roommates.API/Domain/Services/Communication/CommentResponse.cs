using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class CommentResponse : BaseResponse<Comment>
    {
        public CommentResponse(Comment resource) : base(resource)
        {
        }
        public CommentResponse(string message) : base(message)
        {

        }

    }
}
