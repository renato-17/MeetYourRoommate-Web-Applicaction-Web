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
    public class PersonRepository :BaseRepository, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Person> FindById(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> FindByMail(string mail)
        {
            return await _context.People
                .Where(p => p.Mail == mail)
                .FirstOrDefaultAsync();
        }
        public async Task<Person> FindByPhone(string phone)
        {
            return await _context.People
                .Where(p => p.Phone == phone)
                .FirstOrDefaultAsync();
        }

        public async Task<Person> FindByDni(string dni)
        {
            return await _context.People
                .Where(p => p.Dni == dni)
                .FirstOrDefaultAsync(); 
        }
    }
}
