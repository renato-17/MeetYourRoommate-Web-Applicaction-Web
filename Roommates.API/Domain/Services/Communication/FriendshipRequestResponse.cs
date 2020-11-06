using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class FriendshipRequestResponse : BaseResponse<Request>
    {
        public FriendshipRequestResponse(Request resource) : base(resource)
        {
        }

        public FriendshipRequestResponse(string message) : base(message)
        {
        }

        public FriendshipRequestResponse(Request resource, string message) : base(resource, message)
        {
        }
    }
}
