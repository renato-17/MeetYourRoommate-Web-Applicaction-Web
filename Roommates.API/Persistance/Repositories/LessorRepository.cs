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
    public class LessorRepository : BaseRepository, ILessorRepository
    {
        public LessorRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Lessor lessor)
        {
            await _context.Lessors.AddAsync(lessor);
        }

        public async Task<Lessor> FindById(int id)
        {
            return await _context.Lessors.FindAsync(id);
        }

        public async Task<IEnumerable<Lessor>> ListAsync()
        {
            return await _context.Lessors.ToListAsync();
        }

        public void Remove(Lessor lessor)
        {
            _context.Lessors.Remove(lessor);
        }

        public void Update(Lessor lessor)
        {
            _context.Lessors.Update(lessor);
        }
    }
}
