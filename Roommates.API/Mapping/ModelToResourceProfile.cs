
using AutoMapper;
using Roommates.API.Domain.Models;
using Roommates.API.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Student, StudentResource>();
            CreateMap<Lessor, LessorResource>();
            
        }
    }
}
