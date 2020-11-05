using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class LessorResponse : BaseResponse<Lessor>
    {
        public LessorResponse(Lessor resource) : base(resource)
        {
        }

        public LessorResponse(string message) : base(message)
        {
        }
    }
}
