using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface ILessorService
    {
        Task<IEnumerable<Lessor>> ListAsync();
        Task<LessorResponse> GetByIdAsync(int id);
        Task<LessorResponse> SaveAsync(Lessor lessor);
        Task<LessorResponse> UpdateAsync(int id, Lessor lessor);
        Task<LessorResponse> RemoveAsync(int id);
    }
}
