using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class StudyCenterResponse : BaseResponse<StudyCenter>
    {
        public StudyCenterResponse(StudyCenter resource) : base(resource)
        {
        }

        public StudyCenterResponse(string message) : base(message)
        {
        }
    }
}
