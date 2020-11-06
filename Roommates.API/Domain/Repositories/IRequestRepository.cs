using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IRequestRepository
    {
        public Task<Request> FindByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId);
        public Task<IEnumerable<Request>> ListByPersonOneIdAsync(int personOneId);
        public Task<IEnumerable<Request>> ListByPersonTwoIdAsync(int personTwoId);
        public System.Threading.Tasks.Task AddAsync(Request friendshipRequest);
        public void Remove(Request friendshipRequest);
        public void Update(Request friendshipRequest);
    }
}
