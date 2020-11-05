using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IAdService
    {
        Task<IEnumerable<Ad>> ListAsync();
        Task<IEnumerable<Ad>> ListByLessorIdAndPropertyIdAsync(int lessorId, int propertyId);
        Task<AdResponse> GetByIdAndLessorIdAndPropertyIdAsync(int id, int lessorId, int propertyId);
        Task<AdResponse> GetById(int id);
        Task<AdResponse> SaveAsync(Ad ad, int lessorId, int propertyId);
        Task<AdResponse> UpdateAsync(Ad ad, int id, int lessorId, int propertyId);
        Task<AdResponse> DeleteAsync(int id, int lessorId, int propertyId);

        
    }
}
