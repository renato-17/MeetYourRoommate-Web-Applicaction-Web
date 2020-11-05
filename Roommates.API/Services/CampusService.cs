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
    public class CampusService: ICampusService
    {
        private readonly ICampusRepository _campusRepository;
        private readonly IStudyCenterRepository _studyCenterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CampusService(ICampusRepository campusRepository, IUnitOfWork unitOfWork, IStudyCenterRepository studyCenterRepository)
        {
            _campusRepository = campusRepository;
            _unitOfWork = unitOfWork;
            _studyCenterRepository = studyCenterRepository;
        }

        public async Task<IEnumerable<Campus>> ListByStudyCenterIdAsync(int studyCenterId)
        {
            return await _campusRepository.ListByStudyCenterId(studyCenterId);
        }

        public async Task<CampusResponse> GetByIdAndStudyCenterIdAsync(int studyCenterId, int id)
        {
            var existingCampus = await _campusRepository.FindByIdAndStudyCenterId(studyCenterId, id);

            if (existingCampus == null)
                return new CampusResponse("Campus not found");
            return new CampusResponse(existingCampus);
        }

        public async Task<CampusResponse> SaveAsync(int studyCenterId, Campus campus)
        {
            var exisitingStudyCenter = await _studyCenterRepository.FindById(studyCenterId);
            if (exisitingStudyCenter == null)
                return new CampusResponse("Study Center not found");

            campus.StudyCenter = exisitingStudyCenter;
            campus.StudyCenterId = studyCenterId;

            try
            {
                await _campusRepository.AddAsync(campus);
                await _unitOfWork.CompleteAsync();

                return new CampusResponse(campus);
            }
            catch (Exception e)
            {
                return new CampusResponse($"An error ocurred while saving Campus: {e.Message}");
            }
        }

        public async Task<CampusResponse> UpdateAsync(int studyCenterId, int id, Campus campus)
        {
            var existingCampus = await _campusRepository.FindByIdAndStudyCenterId(studyCenterId, id);

            if (existingCampus == null)
                return new CampusResponse("Campus not found");

            existingCampus.Name = campus.Name;

            try
            {
                _campusRepository.Update(existingCampus);
                await _unitOfWork.CompleteAsync();

                return new CampusResponse(existingCampus);
            }
            catch (Exception e)
            {
                return new CampusResponse($"An error ocurred while updating Campus: {e.Message}");
            }
        }

        public async Task<CampusResponse> DeleteAsync(int studyCenterId, int id)
        {
            var existingCampus = await _campusRepository.FindByIdAndStudyCenterId(studyCenterId,id);

            if (existingCampus == null)
                return new CampusResponse("Campus not found");

            try
            {
                _campusRepository.Remove(existingCampus);
                await _unitOfWork.CompleteAsync();

                return new CampusResponse(existingCampus);
            }
            catch (Exception e)
            {
                return new CampusResponse($"An error ocurred while updating Campus: {e.Message}");
            }
        }
    }
}
