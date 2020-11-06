﻿using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IRequestService
    {
        public Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestSent(int personOneId);
        public Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestReceive(int personTwoId);
        public Task<FriendshipRequestResponse> GetByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId);
        public Task<FriendshipRequestResponse> AddTeamRequestAsync(int personOneId, int personTwoId);
        public Task<FriendshipRequestResponse> AddLessorRequestAsync(int personOneId, int personTwoId);
        public Task<FriendshipRequestResponse> AnswerRequest(int personOneId, int id, int answer);
    }
}
