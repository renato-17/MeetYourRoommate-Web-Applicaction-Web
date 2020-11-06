using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class RequestResponse : BaseResponse<Request>
    {
        public RequestResponse(Request resource) : base(resource)
        {
        }

        public RequestResponse(string message) : base(message)
        {
        }

        public RequestResponse(Request resource, string message) : base(resource, message)
        {
        }
    }
}
