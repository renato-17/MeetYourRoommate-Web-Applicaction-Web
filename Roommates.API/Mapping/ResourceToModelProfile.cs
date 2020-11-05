using AutoMapper;
using Roommates.API.Domain.Models;
using Roommates.API.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveStudentResource, Student>();
            CreateMap<SaveLessorResource, Lessor>();

        }
    }
}
