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
    public class CampusRepository : BaseRepository, ICampusRepository
    {
        public CampusRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Campus>> ListByStudyCenterId(int studyCenterId)
        {
            return await _context.Campuses
                .Where(c=>c.StudyCenterId == studyCenterId)
                .Include(c => c.StudyCenter)
                .ToListAsync();
        }

        public async Task<Campus> FindByIdAndStudyCenterId(int studyCenterId, int id)
        {
            return await _context.Campuses
                .Where(c => (c.Id == id) && (c.StudyCenterId == studyCenterId))
                .Include(c => c.StudyCenter)
                .FirstAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Campus campus)
        {
            await _context.Campuses.AddAsync(campus);
        }

        public void Remove(Campus campus)
        {
            _context.Campuses.Remove(campus);
        }

        public void Update(Campus campus)
        {
            _context.Campuses.Update(campus);
        }
    }
}
