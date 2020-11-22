using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IRequestService
    {
        public Task<IEnumerable<Request>> GetSentRequests(int personOneId);
        public Task<IEnumerable<Request>> GetReceivedRequests(int personTwoId);
        public Task<RequestResponse> GetByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId);
        public Task<RequestResponse> AddTeamRequestAsync(int personOneId, int personTwoId);
        public Task<RequestResponse> AddLeaseRequestAsync(int personOneId, int personTwoId, int propertyId);
        public Task<RequestResponse> AnswerRequest(int personOneId, int id, int answer);
        public Task<RequestResponse> DeleteAsync(int personOneId, int personTwoId);
    }
}
