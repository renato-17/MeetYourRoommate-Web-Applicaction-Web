using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class FriendshipRequestResponse : BaseResponse<FriendshipRequest>
    {
        public FriendshipRequestResponse(FriendshipRequest resource) : base(resource)
        {
        }

        public FriendshipRequestResponse(string message) : base(message)
        {
        }

        public FriendshipRequestResponse(FriendshipRequest resource, string message) : base(resource, message)
        {
        }
    }
}
