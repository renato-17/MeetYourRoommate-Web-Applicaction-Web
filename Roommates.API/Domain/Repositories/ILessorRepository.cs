using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface ILessorRepository
    {
        Task<IEnumerable<Lessor>> ListAsync();
        System.Threading.Tasks.Task AddAsync(Lessor lessor);
        Task<Lessor> FindById(int id);
        void Update(Lessor lessor);
        void Remove(Lessor lessor);

    }
}
