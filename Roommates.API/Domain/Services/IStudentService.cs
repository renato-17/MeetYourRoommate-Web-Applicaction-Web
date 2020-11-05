﻿using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> ListAsync();
        Task<StudentResponse> GetByIdAsync(int id);
        Task<StudentResponse> SaveAsync(Student student, int studyCenterId);
        Task<StudentResponse> UpdateAsync(int id, Student student);
        Task<StudentResponse> RemoveAsync(int id);
       
    }
}
