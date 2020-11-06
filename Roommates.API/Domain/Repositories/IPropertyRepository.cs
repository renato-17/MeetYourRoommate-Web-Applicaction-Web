using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> ListAsync();
        Task<Property> FindById(int id);
        Task<IEnumerable<Property>> ListByLessorIdAsync(int lessorId);
        System.Threading.Tasks.Task AddAsync(Property property);
        Task<Property> FindByIdAndLessorId(int lessorId, int id);
        void Update(Property property);
        void Remove(Property property);
    }
}
