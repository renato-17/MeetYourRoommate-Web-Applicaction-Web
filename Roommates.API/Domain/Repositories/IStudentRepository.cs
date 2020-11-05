using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> ListAsync();
        System.Threading.Tasks.Task AddAsync(Student student);
        Task<Student> FindById(int id);
        void Update(Student student);
        void Remove(Student student);
    }
}
