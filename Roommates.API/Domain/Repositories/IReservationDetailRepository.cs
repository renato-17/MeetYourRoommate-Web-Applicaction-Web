using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IReservationDetailRepository
    {
        Task<IEnumerable<ReservationDetail>> ListByReservationIdAsync(int reservationId);
        Task<ReservationDetail> FindByIdAndReservationId(int id, int reservationId);
        System.Threading.Tasks.Task AddAsync(ReservationDetail reservationDetail);
        void Update(ReservationDetail reservationDetail);
        void Remove(ReservationDetail reservationDetail);
    }
}
