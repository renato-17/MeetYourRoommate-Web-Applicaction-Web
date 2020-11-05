using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> ListByTeamIdAsync(int teamId);
        Task<TaskResponse> GetByIdAndTeamIdAsync(int id, int teamId);
        Task<TaskResponse> SaveAsync(Models.Task task, int teamId);
        Task<TaskResponse> UpdateAsync(Models.Task task, int id, int teamId);
        Task<TaskResponse> DeleteAsync(int id, int teamId);
        Task<TaskResponse> FinishAsync(int id, int teamId);
    }
}
