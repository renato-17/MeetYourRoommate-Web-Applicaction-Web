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
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }
        public async Task<Student> FindById(int id)
        {
            return await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Team)
                .Include(s => s.Campus)
                .ThenInclude(c => c.StudyCenter)
                .FirstAsync();

        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _context.Students
                .Include(s => s.Team)
                .Include(s => s.Campus)
                .ThenInclude(c => c.StudyCenter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> ListByTeamIdAsync(int teamId)
        {
            return await _context.Students
                .Where(s => s.TeamId == teamId)
                .Include(s => s.Team)
                .Include(s => s.Campus)
                .ThenInclude(c => c.StudyCenter)
                .ToListAsync();
        }

        public void Remove(Student student)
        {
            _context.Students.Remove(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }


    }
}
