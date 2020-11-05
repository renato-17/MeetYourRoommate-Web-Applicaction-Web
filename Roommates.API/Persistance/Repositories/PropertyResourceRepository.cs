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
    public class PropertyResourceRepository : BaseRepository, IPropertyResourceRepository
    {
        public PropertyResourceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PropertyResource>> ListByPropertyDetailId(int propertyDetailId)
        {
            return await _context.PropertyResources
                .Where(pr => pr.PropertyDetailId == propertyDetailId)
                .ToListAsync();
        }

        public async Task<PropertyResource> FindByIdAndPropertyDetailId(int propertyDetailId, int id)
        {
            return await _context.PropertyResources
                .Where(pr => (pr.PropertyDetailId == propertyDetailId) && (pr.Id == id))
                .FirstAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(PropertyResource propertyResource)
        {
            await _context.PropertyResources.AddAsync(propertyResource);
        }
        public void Update(PropertyResource propertyResource)
        {
            _context.PropertyResources.Update(propertyResource);
        }

        public void Delete(PropertyResource propertyResource)
        {
            _context.PropertyResources.Remove(propertyResource);
        }

    }
}
