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
            return await _context.Ads
                .Where(a => a.Id == id)
                .Include(a => a.Property)
                .FirstAsync();
        }

        public async Task<Ad> FindByIdAndLessorIdAndPropertyId(int id, int lessorId, int propertyId)
        {
            return await _context.Ads.Include(a=>a.Property).SingleAsync(a => (a.Id == id) && (a.LessorId == lessorId) && (a.PropertyId == propertyId));
        }

        public async Task<IEnumerable<Ad>> ListAsync()
        {
            return await _context.Ads.Include(a => a.Property).ToListAsync();
        }

        public async Task<IEnumerable<Ad>> ListByLessorIdAsync(int lessorId)
        {
            return await _context.Ads
                .Where(a => a.LessorId == lessorId)
                .Include(a => a.Property)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ad>> ListByPropertyIdAsync(int propertyId)
        {
            return await _context.Ads.Where(a => (a.PropertyId == propertyId))
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
