using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/teams/{teamId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all tasks by Team",
           Description = "List all tasks by an specific team",
           OperationId = "ListAllTasksByTeamId",
           Tags = new[] { "tasks" }
           )]
        [HttpGet()]
        public async Task<IEnumerable<TaskResource>> GetAllByTeamIdASync(int teamId)
        {
            var tasks = await _taskService.ListByTeamIdAsync(teamId);
            var resources = _mapper.Map<IEnumerable<Domain.Models.Task>, IEnumerable<TaskResource>>(tasks);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get task",
           Description = "Get an specific task by its id and team",
           OperationId = "GetTaskByIdAndTeamId",
           Tags = new[] { "tasks" }
           )]
        [HttpGet("{taskId}")]
        public async Task<TaskResource> GetByIdAndTeamIdASync(int teamId, int taskId)
        {
            var task = await _taskService.GetByIdAndTeamIdAsync(taskId, teamId);
            var resource = _mapper.Map<Domain.Models.Task, TaskResource>(task.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create task",
           Description = "Create a new Task",
           OperationId = "CreateTask",
           Tags = new[] { "tasks" }
           )]
        [HttpPost()]
        public async Task<IActionResult> PostAsync([FromBody] SaveTaskResource resource, int teamId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);

            var result = await _taskService.SaveAsync(task, teamId);

            if (!result.Success)
                return BadRequest(result.Message);

            var taskResource = _mapper.Map<Domain.Models.Task, TaskResource>(task);

            return Ok(taskResource);
        }

        [SwaggerOperation(
               Summary = "Update Task",
               Description = "Update an specific Task",
               OperationId = "UpdateTask",
               Tags = new[] { "tasks" }
               )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveTaskResource resource, int id, int teamId)
        {
            var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);
            var result = await _taskService.UpdateAsync(task,id, teamId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Domain.Models.Task, TaskResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
               Summary = "Finish Task",
               Description = "Update a task's status to finish",
               OperationId = "FinishTask",
               Tags = new[] { "tasks" }
               )]
        [HttpPatch("{id}")]
        public async Task<IActionResult> FinishAsync(int id, int teamId)
        { 

            var result = await _taskService.FinishAsync(id, teamId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Domain.Models.Task, TaskResource>(result.Resource);

            return Ok(studentResource);
        }
    }
}
