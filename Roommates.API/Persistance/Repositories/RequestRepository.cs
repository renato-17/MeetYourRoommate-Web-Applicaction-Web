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
        public  async Task<Request> FindByPersonOneIdAndPersonTwoId(int personOneId, int personTwoId)
        {
            return await _context.Request.FindAsync(personOneId, personTwoId);
        }

        public async Task<IEnumerable<Request>> ListByPersonOneIdAsync(int personOneId)
        {
            return await _context.Request
                .Where(fs => fs.PersonOneId == personOneId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> ListByPersonTwoIdAsync(int personTwoId)
        {
            return await _context.Request
                .Where(fs => fs.PersonTwoId == personTwoId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Request friendshipRequest)
        {
            await _context.AddAsync(friendshipRequest);
        }

        public void Update(Request friendshipRequest)
        {
            _context.Update(friendshipRequest);
        }

        public void Remove(Request friendshipRequest)
        {
            _context.Remove(friendshipRequest);
        }

        public async Task<Request> FindByPersonOneAndPropertyId(int personOneId, int propertyId)
        {
            return await _context.Request
                .Where(r => (r.PersonOneId == personOneId) && (r.PropertyId == propertyId))
                .FirstAsync();
        }
    }
}
