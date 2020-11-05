using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IPropertyDetailRepository
    {
        Task<PropertyDetail> GetPropertyDetail(int propertyId);
        System.Threading.Tasks.Task AddAsync(PropertyDetail propertyDetail);
        void UpdateAsync(PropertyDetail propertyDetail);
        void RemoveAsync(PropertyDetail propertyDetail);
    }
}
