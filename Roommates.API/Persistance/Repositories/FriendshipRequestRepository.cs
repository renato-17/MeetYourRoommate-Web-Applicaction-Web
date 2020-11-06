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
    public class FriendshipRequestRepository : BaseRepository, IFriendshipRequestRepository
    {
        public FriendshipRequestRepository(AppDbContext context) : base(context)
        {
        }
        public  async Task<FriendshipRequest> FindByStudentOneIdAndStudentTwoId(int studentOneId, int studentTwoId)
        {
            return await _context.FriendshipRequests.FindAsync(studentOneId, studentTwoId);
        }

        public async Task<IEnumerable<FriendshipRequest>> ListByStudentOneIdAsync(int studentOneId)
        {
            return await _context.FriendshipRequests
                .Where(fs => fs.StudentOneId == studentOneId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FriendshipRequest>> ListByStudentTwoIdAsync(int studentTwoId)
        {
            return await _context.FriendshipRequests
                .Where(fs => fs.StudentTwoId == studentTwoId)
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
