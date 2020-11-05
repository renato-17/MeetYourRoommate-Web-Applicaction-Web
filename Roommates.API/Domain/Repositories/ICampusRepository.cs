using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface ICampusRepository
    {
        Task<IEnumerable<Campus>> ListByStudyCenterId(int studyCenterId);
        Task<Campus> FindByIdAndStudyCenterId(int studyCenterId, int id);
        System.Threading.Tasks.Task AddAsync(Campus campus);
        void Update(Campus campus);
        void Remove(Campus campus);
    }
}
