using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class PropertyDetailResponse : BaseResponse<PropertyDetail>
    {
        public PropertyDetailResponse(PropertyDetail resource) : base(resource)
        {
        }

        public PropertyDetailResponse(string message) : base(message)
        {
        }

        public PropertyDetailResponse(PropertyDetail resource, string message) : base(resource, message)
        {
        }
    }
}
