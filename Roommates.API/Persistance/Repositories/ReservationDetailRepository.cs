using Microsoft.EntityFrameworkCore;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Persistence.Contexts;
using Roommates.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Persistance.Repositories
{
    public class ReservationDetailRepository : BaseRepository, IReservationDetailRepository
    {
        public ReservationDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task AddAsync(ReservationDetail reservationDetail)
        {
            await _context.ReservationDetails.AddAsync(reservationDetail);
        }

        public async Task<ReservationDetail> FindByIdAndReservationId(int id, int reservationId)
        {
            return await _context.ReservationDetails
                .Where(rd => (rd.Id == id) && (rd.ReservationId == reservationId))
                .FirstAsync();
        }

        public async Task<IEnumerable<ReservationDetail>> ListByReservationIdAsync(int reservationId)
        {
            return await _context.ReservationDetails
                .Where(rd => rd.ReservationId == reservationId)
                .ToListAsync();
        }

        public void Remove(ReservationDetail reservationDetail)
        {
            _context.Remove(reservationDetail);
        }

        public void Update(ReservationDetail reservationDetail)
        {
            _context.Update(reservationDetail);
        }
    }
}
