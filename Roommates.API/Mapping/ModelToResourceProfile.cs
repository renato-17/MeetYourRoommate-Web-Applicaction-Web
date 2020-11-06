
using AutoMapper;
using Roommates.API.Domain;
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
            CreateMap<Property, Resource.PropertyResource>();
            CreateMap<PropertyDetail, PropertyDetailResource>();
            CreateMap<Domain.Models.PropertyResource, PropertyResourceResource>();
            CreateMap<Ad, AdResource>();
            CreateMap<Comment, CommentResource>();
            CreateMap<Team, TeamResource>();
            CreateMap<Domain.Models.Task, TaskResource>();
            CreateMap<StudyCenter, StudyCenterResource>();
            CreateMap<Campus, CampusResource>();
            CreateMap<FriendshipRequest, FriendshipRequestResource>();
            CreateMap<Reservation, ReservationResource>();
            CreateMap<ReservationDetail, ReservationDetailResource>();
        }
    }
}
