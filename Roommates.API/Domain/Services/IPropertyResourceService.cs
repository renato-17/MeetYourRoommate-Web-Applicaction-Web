using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IPropertyResourceService
    {
        Task<IEnumerable<PropertyResource>> ListByPropertyDetailId(int lessorId, int propertyId);
        Task<PropertyResourceResponse> FindByIdAndPropertyDetailId(int lessorId, int propertyId, int id);
        Task<PropertyResourceResponse> SaveAsync(int lessorId, int propertyId, PropertyResource propertyResource);
        Task<PropertyResourceResponse> UpdateAsync(int lessorId, int propertyId, int id, PropertyResource propertyResource);
        Task<PropertyResourceResponse> DeleteAsync(int lessorId, int propertyId, int id); 
    }
}
