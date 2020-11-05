using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IStudyCenterRepository
    {
        Task<IEnumerable<StudyCenter>> ListAsync();
        System.Threading.Tasks.Task AddAsync(StudyCenter studyCenter);
        Task<StudyCenter> FindById(int id);
        void Update(StudyCenter studyCenter);
        void Remove(StudyCenter studyCenter);
    }
}
