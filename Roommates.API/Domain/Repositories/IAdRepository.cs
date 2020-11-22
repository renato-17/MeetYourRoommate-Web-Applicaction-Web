using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IAdRepository
    {
        Task<IEnumerable<Ad>> ListAsync();
        Task<IEnumerable<Ad>> ListByPropertyIdAsync(int propertyId);
        Task<IEnumerable<Ad>> ListByLessorIdAsync(int lessorId);
        System.Threading.Tasks.Task AddAsync(Ad ad);
        Task<Ad> FindByIdAndLessorIdAndPropertyId(int id, int lessorId, int propertyId);
        Task<Ad> FindById(int id);
        void Update(Ad ad);
        void Remove(Ad ad);
    }
}
