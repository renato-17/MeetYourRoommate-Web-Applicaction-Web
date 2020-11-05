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
    public class LessorService : ILessorService
    {
        private readonly ILessorRepository _lessorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LessorService(ILessorRepository lessorRepository, IUnitOfWork unitOfWork)
        {
            _lessorRepository = lessorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessorResponse> GetByIdAsync(int id)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");
            return new LessorResponse(existingLessor);
        }

        public async Task<IEnumerable<Lessor>> LystAsync()
        {
            return await _lessorRepository.ListAsync();
        }

        public async Task<LessorResponse> RemoveAsync(int id)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");
            try
            {
                _lessorRepository.Remove(existingLessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(existingLessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while removing lessor: {ex.Message}");
            }
        }

        public async Task<LessorResponse> SaveAsync(Lessor lessor)
        {
            try
            {
                await _lessorRepository.AddAsync(lessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(lessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while saving lessor: {ex.InnerException} ");
            }
        }

        public async Task<LessorResponse> UpdateAsync(int id, Lessor lessor)
        {
            var existingLessor = await _lessorRepository.FindById(id);
            if (existingLessor == null)
                return new LessorResponse("Lessor not found");

            existingLessor.FirstName = lessor.FirstName;
            existingLessor.LastName = lessor.LastName;
            existingLessor.Dni = lessor.Dni;
            existingLessor.Phone = lessor.Phone;
            existingLessor.Gender = lessor.Gender;
            existingLessor.Address = lessor.Address;
            existingLessor.Birthdate = lessor.Birthdate;
            existingLessor.Premium = lessor.Premium;

            try
            {
                _lessorRepository.Update(existingLessor);
                await _unitOfWork.CompleteAsync();
                return new LessorResponse(existingLessor);
            }
            catch(Exception ex)
            {
                return new LessorResponse($"An error ocurred while updating lessor: {ex.InnerException}");
            }

        }
    }
}
