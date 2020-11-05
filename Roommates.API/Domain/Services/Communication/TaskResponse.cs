using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class TaskResponse : BaseResponse<Models.Task>
    {
        public TaskResponse(Models.Task resource) : base(resource)
        {
        }

        public TaskResponse(string message) : base(message)
        {
        }

        public TaskResponse(Models.Task resource, string message) : base(resource, message)
        {
        }
    }
}
