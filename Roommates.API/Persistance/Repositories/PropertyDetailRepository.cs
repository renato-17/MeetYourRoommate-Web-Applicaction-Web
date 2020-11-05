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
    public class PropertyDetailRepository : BaseRepository, IPropertyDetailRepository
    {
        public PropertyDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(PropertyDetail propertyDetail)
        {
            await _context.PropertyDetails.AddAsync(propertyDetail);
        }

        public async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            return await _context.PropertyDetails
                .Where(pd => pd.PropertyId == propertyId)
                .FirstAsync();
        }

        public void RemoveAsync(PropertyDetail propertyDetail)
        {
            _context.PropertyDetails.Remove(propertyDetail);
        }

        public void UpdateAsync(PropertyDetail propertyDetail)
        {
            _context.PropertyDetails.Update(propertyDetail);
        }
    }
}
