using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Persistance.Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context)
        {
        }
        public  async Task<FriendshipRequest> FindByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId)
        {
            return await _context.FriendshipRequests.FindAsync(personOneId, personTwoId);
        }

        public async Task<IEnumerable<FriendshipRequest>> ListByPersonOneIdAsync(int personOneId)
        {
            return await _context.FriendshipRequests
                .Where(fs => fs.PersonOneId == personOneId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FriendshipRequest>> ListByPersonTwoIdAsync(int personTwoId)
        {
            return await _context.FriendshipRequests
                .Where(fs => fs.PersonTwoId == personTwoId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(FriendshipRequest friendshipRequest)
        {
            await _context.AddAsync(friendshipRequest);
        }

        public void Update(FriendshipRequest friendshipRequest)
        {
            _context.Update(friendshipRequest);
        }

        public void Remove(FriendshipRequest friendshipRequest)
        {
            _context.Remove(friendshipRequest);
        }

     
    }
}
