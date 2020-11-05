using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IStudyCenterService
    {
        Task<IEnumerable<StudyCenter>> ListAsync();
        Task<StudyCenterResponse> GetByIdAsync (int id);
        Task<StudyCenterResponse> SaveAsync(StudyCenter studyCenter);
        Task<StudyCenterResponse> UpdateAsync(int id, StudyCenter studyCenter);
        Task<StudyCenterResponse> DeleteAsync(int id);

    }
}
