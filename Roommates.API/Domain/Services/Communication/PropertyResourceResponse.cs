using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class PropertyResourceResponse : BaseResponse<PropertyResource>
    {
        public PropertyResourceResponse(PropertyResource resource) : base(resource)
        {
        }

        public PropertyResourceResponse(string message) : base(message)
        {
        }

        public PropertyResourceResponse(PropertyResource resource, string message) : base(resource, message)
        {
        }
    }
}
