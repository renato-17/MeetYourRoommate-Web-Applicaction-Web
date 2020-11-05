using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> ListAsync();
        Task<TeamResponse> GetByIdAsync(int id);
        Task<TeamResponse> GetByNameAsync(Team team);
        Task<TeamResponse> GetByStudentId(int studentId);
        Task<TeamResponse> SaveAsync(Team team);
        Task<TeamResponse> UpdateAsync(Team team, int id);
        Task<TeamResponse> RemoveAsync(int id);
    }
}
