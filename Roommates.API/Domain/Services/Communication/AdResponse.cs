using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class AdResponse : BaseResponse<Ad>
    {
        public AdResponse(Ad resource) : base(resource)
        {
        }
        public AdResponse(string message) : base(message)
        {
        }
        public AdResponse(Ad resource, string message) : base(resource, message)
        {
        }
    }
}
