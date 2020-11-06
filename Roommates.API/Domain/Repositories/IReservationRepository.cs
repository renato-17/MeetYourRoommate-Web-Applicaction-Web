using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> ListAsync();
        Task<Reservation> FindById(int id);
        System.Threading.Tasks.Task AddAsync(Reservation reservation);
        void Update(Reservation reservation);
        void Remove(Reservation reservation);
    }
}
