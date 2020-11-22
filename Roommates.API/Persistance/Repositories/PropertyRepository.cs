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
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {

        }

        public async System.Threading.Tasks.Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
        }

        public async Task<Property> FindByIdAndLessorId(int lessorId,int id)
        {
            return await _context.Properties
                .Where(p => (p.Id == id) && (p.LessorId == lessorId))
                .Include(p => p.Lessor)
                .Include(p=>p.PropertyDetail)
                .FirstAsync();
                
        }

        public async Task<IEnumerable<Property>> ListByLessorIdAsync(int lessorId)
        {
            return await _context.Properties
                .Where(p => p.LessorId == lessorId)
                .Include(p => p.PropertyDetail)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> ListAsync()
        {
            return await _context.Properties
                .Include(p => p.Lessor)
                .Include(p => p.PropertyDetail)
                .ToListAsync(); 
                
        }

        public void Remove(Property property)
        {
            _context.Properties.Remove(property);
        }

        public void Update(Property property)
        {
            _context.Properties.Update(property);
        }

        public async Task<Property> FindById(int id)
        {
            return await _context.Properties
                .Where(p => p.Id == id)
                .Include(p => p.Lessor)
                .FirstAsync();
        }
    }
}
