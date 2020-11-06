using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IFriendshipRequestService
    {
        public Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestSent(int studentOneId);
        public Task<IEnumerable<FriendshipRequest>> GetFriendshipRequestReceive(int studentTwoId);
        public Task<FriendshipRequestResponse> GetByStudentOneIdAndStudentTwoId(int studentOneId, int studentTwoId);
        public Task<FriendshipRequestResponse> AddAsync(int studentOneId, int studentTwoId);
        public Task<FriendshipRequestResponse> AnswerRequest(int studentOneId, int id, int answer);
    }
}
