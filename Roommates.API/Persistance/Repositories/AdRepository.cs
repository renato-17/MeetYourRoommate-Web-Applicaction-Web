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
    public class AdRepository : BaseRepository, IAdRepository
    {
        public AdRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Ad ad)
        {
            await _context.Ads.AddAsync(ad);
        }

        public async Task<Ad> FindById(int id)
        {
            return await _context.Ads.FindAsync(id);
        }

        public async Task<Ad> FindByIdAndLessorIdAndPropertyId(int id, int lessorId, int propertyId)
        {
            return await _context.Ads.SingleAsync(a => (a.Id == id) && (a.LessorId == lessorId) && (a.PropertyId == propertyId));
        }

        public async Task<IEnumerable<Ad>> ListAsync()
        {
            return await _context.Ads.ToListAsync(); 
        }

        public async Task<IEnumerable<Ad>> ListByLessorIdAndPropertyIdAsync(int lessorId, int propertyId)
        {
            return await _context.Ads.Where(a => (a.PropertyId == propertyId && a.LessorId ==lessorId))
                .Include(a => a.Property) 
                .ToListAsync();
        }

        public void Remove(Ad ad)
        {
            _context.Ads.Remove(ad);
        }

        public void Update(Ad ad)
        {
            _context.Ads.Update(ad);
        }
    }
}
