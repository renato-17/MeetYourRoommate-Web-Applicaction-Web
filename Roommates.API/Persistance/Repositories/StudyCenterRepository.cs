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
    public class StudyCenterRepository : BaseRepository, IStudyCenterRepository
    {
        public StudyCenterRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(StudyCenter studyCenter)
        {
            await _context.StudyCenters.AddAsync(studyCenter);
        }

        public async Task<StudyCenter> FindById(int id)
        {
            return await _context.StudyCenters.FindAsync(id);
        }

        public async Task<IEnumerable<StudyCenter>> ListAsync()
        {
            return await _context.StudyCenters.ToListAsync();
        }

        public void Remove(StudyCenter studyCenter)
        {
            _context.StudyCenters.Remove(studyCenter);
        }

        public void Update(StudyCenter studyCenter)
        {
            _context.StudyCenters.Update(studyCenter);
        }
    }
}
