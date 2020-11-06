using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services
{
    public interface IReservationDetailService
    {
        Task<IEnumerable<ReservationDetail>> ListByReservationIdAsync(int reservationId);
        Task<ReservationDetailResponse> GetByIdAndReservationIdAsync(int id, int reservationId);
        Task<ReservationDetailResponse> SaveAsync(ReservationDetail reservationDetail, int reservationId, int studentId, int propertyId);
        Task<ReservationDetailResponse> UpdateAsync(ReservationDetail reservationDetail, int id, int reservationId);
        Task<ReservationDetailResponse> DeleteAsync(int id, int reservationId);
    }
}
