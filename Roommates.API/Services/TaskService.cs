using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork, ITeamRepository teamRepository)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _teamRepository = teamRepository;
        }


        public async Task<IEnumerable<Domain.Models.Task>> ListByTeamIdAsync(int teamId)
        {
            return await _taskRepository.ListByTeamIdAsync(teamId);
        }

        public async Task<TaskResponse> GetByIdAndTeamIdAsync(int id, int teamId)
        {
            var existingTask = await _taskRepository.FindByIdAndTeamId(id, teamId);

            if (existingTask == null)
                return new TaskResponse("Task or Team not found");

            return new TaskResponse(existingTask); 
        }
        public async Task<TaskResponse> SaveAsync(Domain.Models.Task task, int teamId)
        {
            var existingTeam = await _teamRepository.FindById(teamId);
            if (existingTeam == null)
                return new TaskResponse("Team not found");

            task.Team = existingTeam;
            task.TeamId = existingTeam.Id;
            task.Active = true;

            try
            {
        
                await _taskRepository.AddAsync(task);
                await _unitOfWork.CompleteAsync();

                return new TaskResponse(task);

            }
            catch (Exception ex)
            {
                return new TaskResponse($"An error ocurred while saving task: {ex.Message}");
            }
        }

        public async Task<TaskResponse> UpdateAsync(Domain.Models.Task task, int id, int teamId)
        {

            var existingTeam = await _teamRepository.FindById(teamId);
            if (existingTeam == null)
                return new TaskResponse("Team not found");

            var existingTask = await _taskRepository.FindByIdAndTeamId(id, teamId);
            if (existingTask == null)
                return new TaskResponse("Task not found");

            if (!existingTask.Active)
                return new TaskResponse("Task is already finish");

            existingTask.Description = task.Description;

            try
            {
                _taskRepository.Update(existingTask);
                await _unitOfWork.CompleteAsync();

                return new TaskResponse(existingTask);
            }
            catch(Exception ex)
            {
                return new TaskResponse($"An error ocurred while updating task: {ex.Message}");
            }
        }

        public async Task<TaskResponse> DeleteAsync(int id, int teamId)
        {
            var existingTask = await _taskRepository.FindByIdAndTeamId(id, teamId);
            
            if (existingTask == null)
                return new TaskResponse("Task not found");

            try
            {
                _taskRepository.Remove(existingTask);
                await _unitOfWork.CompleteAsync();

                return new TaskResponse(existingTask);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"An error ocurred while deleting task: {ex.Message}");
            }
        }

        public async Task<TaskResponse> FinishAsync(int id, int teamId)
        {
            
            var existingTask = await _taskRepository.FindByIdAndTeamId(id, teamId);
            if (existingTask == null)
                return new TaskResponse("Task or Team not found");

            if (!existingTask.Active)
                return new TaskResponse("Task is already finish");

            existingTask.Active = false;

            try
            {
                _taskRepository.Update(existingTask);
                await _unitOfWork.CompleteAsync();

                return new TaskResponse(existingTask);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"An error ocurred while finishing task: {ex.Message}");
            }
        }
    }
}
