using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Persistance.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Domain.Models.Task task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task<Domain.Models.Task> FindByIdAndTeamId(int id, int teamId)
        {
            return await _context.Tasks
                .SingleAsync(t => (t.Id == id) && (t.TeamId == teamId));
        }
 
        public async Task<IEnumerable<Domain.Models.Task>> ListByTeamIdAsync(int teamId)
        {
            return await _context.Tasks
                .Where(t => t.TeamId == teamId)
                .Include(t => t.Team)
                .ToListAsync();
        }

        public void Remove(Domain.Models.Task task)
        {
            _context.Tasks.Remove(task);
        }

        public void Update(Domain.Models.Task task)
        {
            _context.Tasks.Update(task);
        }
    }
}
