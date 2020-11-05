using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class PropertyResponse : BaseResponse<Property>
    {
        public PropertyResponse(Property resource) : base(resource)
        {
        }
        public PropertyResponse(string message) : base(message)
        {
        }
    }
}
