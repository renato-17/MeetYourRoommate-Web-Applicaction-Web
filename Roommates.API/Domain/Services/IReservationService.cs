using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> ListAsync();
        Task<ReservationResponse> GetByIdAsync(int id);
        Task<ReservationResponse> SaveAsync(Reservation reservation);
        Task<ReservationResponse> UpdateAsync(int id, Reservation reservation);
        Task<ReservationResponse> DeleteAsync(int id);
    }
}
