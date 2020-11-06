using Org.BouncyCastle.X509;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailRepository _reservationDetailRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationDetailService(IReservationDetailRepository reservationDetailRepository, IReservationRepository reservationRepository, IPropertyRepository propertyRepository, IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _reservationDetailRepository = reservationDetailRepository;
            _reservationRepository = reservationRepository;
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }



        public async Task<ReservationDetailResponse> GetByIdAndReservationIdAsync(int id, int reservationId)
        {
            var existingReservationDetail = await _reservationDetailRepository.FindByIdAndReservationId(id, reservationId);
            if (existingReservationDetail == null)
                return new ReservationDetailResponse("Reservation detail not found");

            return new ReservationDetailResponse(existingReservationDetail);
        }

        public async Task<IEnumerable<ReservationDetail>> ListByReservationIdAsync(int reservationId)
        {
            return await _reservationDetailRepository.ListByReservationIdAsync(reservationId);
        }

        public async Task<ReservationDetailResponse> SaveAsync(ReservationDetail reservationDetail, int reservationId, int studentId, int propertyId)
        {
            var reservation = await _reservationRepository.FindById(reservationId);
            if (reservation == null)
                return new ReservationDetailResponse("Reservation not found");

            var student = await _studentRepository.FindById(studentId);
            if (student == null)
                return new ReservationDetailResponse("Student not found");

            var property = await _propertyRepository.FindById(propertyId);
            if (property == null)
                return new ReservationDetailResponse("Property not found");

            reservationDetail.Reservation = reservation;
            reservationDetail.ReservationId = reservation.Id;
            reservationDetail.Student = student;
            reservationDetail.StudentId = student.Id;
            reservationDetail.Property = property;
            reservationDetail.PropertyId = propertyId;
            reservationDetail.Lessor = property.Lessor;
            reservationDetail.LessorId = property.Lessor.Id;

            try
            {
                await _reservationDetailRepository.AddAsync(reservationDetail);
                await _unitOfWork.CompleteAsync();
                return new ReservationDetailResponse(reservationDetail);
            }catch (Exception ex)
            {
                return new ReservationDetailResponse($"An error ocurred while saving a new reservation detail: {ex.InnerException}");
            }
            
        }

        public async Task<ReservationDetailResponse> UpdateAsync(ReservationDetail reservationDetail, int id, int reservationId)
        {
            var existingReservationDetail = await _reservationDetailRepository.FindByIdAndReservationId(id, reservationId);
            if (existingReservationDetail == null)
                return new ReservationDetailResponse("Reservation detail not found");

            existingReservationDetail.Amount = reservationDetail.Amount;
            existingReservationDetail.Downpayment = reservationDetail.Downpayment;

            try
            {
                _reservationDetailRepository.Update(existingReservationDetail);
                await _unitOfWork.CompleteAsync();

                return new ReservationDetailResponse(existingReservationDetail);
            }
            catch (Exception ex)
            {
                return new ReservationDetailResponse($"An error ocurred while updating a new reservation detail: {ex.Message}");
            }
        }

        public async Task<ReservationDetailResponse> DeleteAsync(int id, int reservationId)
        {
            var existingReservationDetail = await _reservationDetailRepository.FindByIdAndReservationId(id, reservationId);
            if (existingReservationDetail == null)
                return new ReservationDetailResponse("Reservation detail not found");

            try
            {
                _reservationDetailRepository.Update(existingReservationDetail);
                await _unitOfWork.CompleteAsync();

                return new ReservationDetailResponse(existingReservationDetail);
            }
            catch (Exception ex)
            {
                return new ReservationDetailResponse($"An error ocurred deleting updating a new reservation detail: {ex.Message}");
            }
        }

    }
}
