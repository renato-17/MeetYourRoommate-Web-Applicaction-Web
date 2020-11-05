using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface ICampusService
    {
        Task<IEnumerable<Campus>> ListByStudyCenterIdAsync(int studyCenterId);
        Task<CampusResponse> GetByIdAndStudyCenterIdAsync(int studyCenterId, int id);
        Task<CampusResponse> SaveAsync(int studyCenterId, Campus campus);
        Task<CampusResponse> UpdateAsync(int studyCenterId, int id, Campus campus);
        Task<CampusResponse> DeleteAsync(int studyCenterId, int id);
    }
}
