using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IPropertyDetailService
    {
        Task<PropertyDetailResponse> GetPropertyDetailAsync(int lessorId, int propertyId);
        Task<PropertyDetailResponse> SaveAsync(int lessorId, int propertyId, PropertyDetail propertyDetail);
        Task<PropertyDetailResponse> UpdateAsync(int lessorId, int propertyId, PropertyDetail propertyDetail);
        Task<PropertyDetailResponse> DeleteAsync(int lessorId, int propertyId);
    }
}
