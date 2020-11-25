using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> ListAsync();
        Task<Person> FindById(int id);
        Task<Person> FindByDni(string dni);
        Task<Person> FindByMail(string mail);
        Task<Person> FindByPhone(string phone);
    }
}
