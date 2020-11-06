using Roommates.API.Domain;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ReservationResponse> GetByIdAsync(int id)
        {
            var existingReservation = await _reservationRepository.FindById(id);

            if (existingReservation == null)
                return new ReservationResponse("Reservation not found");

            return new ReservationResponse(existingReservation);
        }

        public async Task<IEnumerable<Reservation>> ListAsync()
        {
            return await _reservationRepository.ListAsync();
        }

        public async Task<ReservationResponse> SaveAsync(Reservation reservation)
        {
            try
            {
                await _reservationRepository.AddAsync(reservation);
                await _unitOfWork.CompleteAsync();

                return new ReservationResponse(reservation);

            }catch(Exception ex)
            {
                return new ReservationResponse($"An error ocurred while saving reservation: {ex.Message}");
            }
        }

        public async Task<ReservationResponse> UpdateAsync(int id, Reservation reservation)
        {
            var existingReservation = await _reservationRepository.FindById(id);

            if (existingReservation == null)
                return new ReservationResponse("Reservation not found");

            existingReservation.DateEnd = reservation.DateEnd;
            existingReservation.DateStart = reservation.DateStart;

            try
            {
                _reservationRepository.Update(reservation);
                await _unitOfWork.CompleteAsync();

                return new ReservationResponse(reservation);

            }
            catch (Exception ex)
            {
                return new ReservationResponse($"An error ocurred while updating reservation: {ex.Message}");
            }
        }

        public async Task<ReservationResponse> DeleteAsync(int id)
        {
            var existingReservation = await _reservationRepository.FindById(id);

            if (existingReservation == null)
                return new ReservationResponse("Reservation not found");


            try
            {
                _reservationRepository.Update(existingReservation);
                await _unitOfWork.CompleteAsync();

                return new ReservationResponse(existingReservation);

            }
            catch (Exception ex)
            {
                return new ReservationResponse($"An error ocurred while updating reservation: {ex.Message}");
            }
        }
    }
}
