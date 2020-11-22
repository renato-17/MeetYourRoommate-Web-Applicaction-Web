using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> ListAsync();
        Task<PropertyResponse> GetByIdAsync(int id);
        Task<PropertyResponse> SaveAsync(int lessorId, Property property);
        Task<PropertyResponse> UpdateAsync(int lessorId,int id, Property property);
        Task<PropertyResponse> RemoveAsync(int id);

        Task<IEnumerable<Property>> ListByLessorIdAsync(int lessorId);
        Task<PropertyResponse> GetByIdAndLessorIdAsync(int lessorId, int id);
    }
}
