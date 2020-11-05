using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class TeamResponse : BaseResponse<Team>
    {
        public TeamResponse(Team resource) : base(resource)
        {
        }

        public TeamResponse(string message) : base(message)
        {
        }

        public TeamResponse(Team resource, string message) : base(resource, message)
        {
        }
    }
}
