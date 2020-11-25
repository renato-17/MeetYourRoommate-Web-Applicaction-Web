
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Roommates.API.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using Roommates.API.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roommates.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Get all students",
           Description = "Get all students",
           OperationId = "GetAllStudents",
           Tags = new[] { "students" }
           )]
        [HttpGet]
        public async Task<IEnumerable<StudentResource>> GetAllAsync()
        {
            var students = await _studentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get all students by Team",
           Description = "Get all students by an specific Team",
           OperationId = "GetAllStudentsByTeam",
           Tags = new[] { "students" }
           )]
        [HttpGet("teams/{teamId}")]
        public async Task<IEnumerable<StudentResource>> GetAllByTeamIdAsync(int teamId)
        {
            var students = await _studentService.GetAllStudentsByTeamId(teamId);
            var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
            return resources;
        }

        [SwaggerOperation(
           Summary = "Get Student",
           Description = "Get Student by id",
           OperationId = "GetStudentById",
           Tags = new[] { "students" }
           )]
        [HttpGet("{id}")]
        public async Task<StudentResource> GetByStudentId(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            var resource = _mapper.Map<Student, StudentResource>(student.Resource);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Create Student",
           Description = "Create a new Student",
           OperationId = "CreateStudent",
           Tags = new[] { "students" }
           )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var student = _mapper.Map<SaveStudentResource, Student>(resource);

            var result = await _studentService.SaveAsync(student, resource.StudyCenterId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }

        [SwaggerOperation(
           Summary = "Join team",
           Description = "Join a team",
           OperationId = "JoinTeam",
           Tags = new[] { "students" }
           )]
        [HttpPost("{id}")]
        public async Task<IActionResult> JointTeam([FromBody] SaveTeamResource resource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var team = _mapper.Map<SaveTeamResource, Team>(resource);

            var result = await _studentService.JoinTeam(team, id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
           Summary = "Update Student",
           Description = "Update an specific Student",
           OperationId = "UpdateStudent",
           Tags = new[] { "students" }
           )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveStudentResource resource, int id)
        {
            var student = _mapper.Map<SaveStudentResource, Student>(resource);
            var result = await _studentService.UpdateAsync(id, student);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
           Summary = "Leave team",
           Description = "Leave a team",
           OperationId = "LeaveTeam",
           Tags = new[] { "students" }
           )]
        [HttpPatch("{id}")]
        public async Task<IActionResult> LeaveTeam(int id)
        {

            var result = await _studentService.LeaveTeam(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
           Summary = "Delete Student",
           Description = "Delete an specific Student",
           OperationId = "DeleteStudent",
           Tags = new[] { "students" }
           )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _studentService.RemoveAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

            return Ok(studentResource);
        }

        [SwaggerOperation(
           Summary = "Authenticate Student",
           Description = "Authenticate an specific Student",
           OperationId = "AuthenticateStudent",
           Tags = new[] { "students" }
           )]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response =  await _studentService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Invalid Username or Password" });

            return Ok(response);
        }
    }
}
