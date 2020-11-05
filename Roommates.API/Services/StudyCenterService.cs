using Microsoft.AspNetCore.Http.Connections;
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
    public class StudyCenterService : IStudyCenterService
    {
        private readonly IStudyCenterRepository _studyCenterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public StudyCenterService(IStudyCenterRepository studyCenterRepository, IUnitOfWork unitOfWork)
        {
            _studyCenterRepository = studyCenterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudyCenter>> ListAsync()
        {
            return await _studyCenterRepository.ListAsync();
        }

        public async Task<StudyCenterResponse> GetByIdAsync(int id)
        {
            var existingStudyCenter = await _studyCenterRepository.FindById(id);

            if (existingStudyCenter == null)
                return new StudyCenterResponse("Study Center not found");
            return new StudyCenterResponse(existingStudyCenter);
        }

        public async Task<StudyCenterResponse> SaveAsync(StudyCenter studyCenter)
        {
            try
            {
                await _studyCenterRepository.AddAsync(studyCenter);
                await _unitOfWork.CompleteAsync();

                return new StudyCenterResponse(studyCenter);
            }
            catch (Exception e)
            {
                return new StudyCenterResponse($"An error ocurred while saving Study Center: {e.Message}");
            }
        }

        public async Task<StudyCenterResponse> UpdateAsync(int id, StudyCenter studyCenter)
        {
            var existingStudyCenter = await _studyCenterRepository.FindById(id);

            if (existingStudyCenter == null)
                return new StudyCenterResponse("Study Center not found");

            existingStudyCenter.Name = studyCenter.Name;

            try
            {
                _studyCenterRepository.Update(existingStudyCenter);
                await _unitOfWork.CompleteAsync();

                return new StudyCenterResponse(existingStudyCenter);
            }
            catch (Exception e)
            {
                return new StudyCenterResponse($"An error ocurred while updating Study Center: {e.Message}");
            }
        }

        public async Task<StudyCenterResponse> DeleteAsync(int id)
        {
            var existingStudyCenter = await _studyCenterRepository.FindById(id);

            if (existingStudyCenter == null)
                return new StudyCenterResponse("Study Center not found");

            try
            {
                _studyCenterRepository.Remove(existingStudyCenter);
                await _unitOfWork.CompleteAsync();

                return new StudyCenterResponse(existingStudyCenter);
            }
            catch (Exception e)
            {
                return new StudyCenterResponse($"An error ocurred while deleting Study Center: {e.Message}");
            }
        }
    }
}
