
using Roommates.API.Domain.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IPropertyResourceRepository
    {
        Task<IEnumerable<PropertyResource>> ListByPropertyDetailId(int propertyDetailId);
        Task<PropertyResource> FindByIdAndPropertyDetailId(int propertyDetailId, int id);
        System.Threading.Tasks.Task AddAsync(Models.PropertyResource propertyResource);
        void Update(Models.PropertyResource propertyResource);
        void Delete(Models.PropertyResource propertyResource);
    }
}
