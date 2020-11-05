using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class StudentResponse : BaseResponse<Student>
    {
        public StudentResponse(Student resource) : base(resource)
        {
        }

        public StudentResponse(string message) : base(message)
        {
        }

        public StudentResponse(Student resource, string message) : base(resource, message)
        {
        }
    }
}
