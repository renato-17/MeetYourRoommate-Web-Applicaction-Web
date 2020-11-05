using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> ListByTeamIdAsync(int teamId);
        Task<Models.Task> FindByIdAndTeamId(int id, int teamId);
        System.Threading.Tasks.Task AddAsync(Models.Task team);
        void Update(Models.Task task);
        void Remove(Models.Task task);
    }
}
 