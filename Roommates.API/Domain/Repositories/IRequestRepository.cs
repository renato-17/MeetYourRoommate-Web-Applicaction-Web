using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IRequestRepository
    {
        public Task<FriendshipRequest> FindByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId);
        public Task<IEnumerable<FriendshipRequest>> ListByPersonOneIdAsync(int personOneId);
        public Task<IEnumerable<FriendshipRequest>> ListByPersonTwoIdAsync(int personTwoId);
        public System.Threading.Tasks.Task AddAsync(FriendshipRequest friendshipRequest);
        public void Remove(FriendshipRequest friendshipRequest);
        public void Update(FriendshipRequest friendshipRequest);
    }
}
