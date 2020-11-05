using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class CampusResponse : BaseResponse<Campus>
    {
        public CampusResponse(Campus resource) : base(resource)
        {
        }

        public CampusResponse(string message) : base(message)
        {
        }
    }
}
