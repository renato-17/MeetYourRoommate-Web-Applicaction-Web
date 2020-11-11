using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class PersonResponse : BaseResponse<Person>
    {
        public PersonResponse(Person resource) : base(resource)
        {
        }

        public PersonResponse(string message) : base(message)
        {
        }

        public PersonResponse(Person resource, string message) : base(resource, message)
        {
        }
    }
}
