using Microsoft.AspNetCore.Mvc;
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
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IStudentRepository _studentRepository;
        public readonly IUnitOfWork _unitOfWork;

        public TeamService(ITeamRepository teamRepository, IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Team>> ListAsync()
        {
            return await _teamRepository.ListAsync();
        }

        public async Task<TeamResponse> GetByIdAsync(int id)
        {
            var existingTeam = await _teamRepository.FindById(id);

            if (existingTeam == null)
                return new TeamResponse("Team not found");

            return new TeamResponse(existingTeam);
        }

        public async Task<TeamResponse> GetByStudentId(int studentId)
        {
            var existingStudent = await _studentRepository.FindById(studentId);

            if (existingStudent == null)
                return new TeamResponse($"Student with id: {studentId} not found");

            if (existingStudent.TeamId.HasValue)
            {
                var existingTeam = await _teamRepository.FindById(existingStudent.TeamId.Value);
                return new TeamResponse(existingTeam);
            }

            return new TeamResponse("Student does not have team");
        }

     
        public async Task<TeamResponse> SaveAsync(Team team)
        {
            try
            {
                await _teamRepository.AddAsync(team);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(team);
            
            }catch(Exception ex)
            {
                return new TeamResponse($"An error ocurred while saving team: {ex.Message}");
            }
        }

        public async Task<TeamResponse> RemoveAsync(int id)
        {
            var existingTeam = await _teamRepository.FindById(id);

            if (existingTeam == null)
                return new TeamResponse("Team not found");

            try
            {
                _teamRepository.Remove(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);

            }catch(Exception ex)
            {
                return new TeamResponse($"An error ocurred while saving team: {ex.Message}");
            }
        }
        public async Task<TeamResponse> UpdateAsync(Team team, int id)
        {
            var existingTeam = await _teamRepository.FindById(id);

            if (existingTeam == null)
                return new TeamResponse("Team not found");

            existingTeam.Name = team.Name;

            try
            {
                _teamRepository.Update(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);

            }
            catch (Exception ex)
            {
                return new TeamResponse($"An error ocurred while saving team: {ex.Message}");
            }
        }

        public async Task<TeamResponse> GetByNameAsync(Team team)
        {
            var existingTeam = await _teamRepository.FindByName(team.Name);

            if (existingTeam == null)
                return new TeamResponse("Team not found");

            return new TeamResponse(existingTeam);
        }
    }
}
