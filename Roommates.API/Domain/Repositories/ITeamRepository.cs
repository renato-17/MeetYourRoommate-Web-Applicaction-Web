using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> ListAsync();
        Task<Team> FindByName(string name);
        System.Threading.Tasks.Task AddAsync(Team team);
        Task<Team> FindById(int id);
        void Update(Team team);
        void Remove(Team team);
    }
}
