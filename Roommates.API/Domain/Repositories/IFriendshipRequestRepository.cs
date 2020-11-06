using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IFriendshipRequestRepository
    {
        public Task<FriendshipRequest> FindByStudentOneIdAndStudentTwoId(int studentOneId, int studentTwoId);
        public Task<IEnumerable<FriendshipRequest>> ListByStudentOneIdAsync(int studentOneId);
        public Task<IEnumerable<FriendshipRequest>> ListByStudentTwoIdAsync(int studentTwoId);
        public System.Threading.Tasks.Task AddAsync(FriendshipRequest friendshipRequest);
        public void Remove(FriendshipRequest friendshipRequest);
        public void Update(FriendshipRequest friendshipRequest);
    }
}
