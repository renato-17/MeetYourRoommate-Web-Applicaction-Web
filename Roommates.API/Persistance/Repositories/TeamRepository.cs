using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Persistance.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
        }

        public async Task<Team> FindById(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> FindByName(string name)
        {
            try
            {
                return await _context.Teams
                    .SingleAsync(t => t.Name.Equals(name));

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Team>> ListAsync()
        { 
            return await _context.Teams.ToListAsync();
        }

        public void Remove(Team team)
        {
            _context.Teams.Remove(team);
        }

        public void Update(Team team)
        {
            _context.Teams.Update(team);
        }
    }
}
