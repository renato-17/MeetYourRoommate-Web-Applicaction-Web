
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Roommates.API.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roommates.API.Controllers
{

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

        [HttpGet]
        public async Task<IEnumerable<StudentResource>> GetAllAsync()
        {
            var students = await _studentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<StudentResource> GetByStudentId(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            var resource = _mapper.Map<Student, StudentResource>(student.Resource);
            return resource;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var student = _mapper.Map<SaveStudentResource, Student>(resource);

            var result = await _studentService.SaveAsync(student,resource.StudyCenterId);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(student);

            return Ok(studentResource);
        }


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



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _studentService.RemoveAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);
            
            return Ok(studentResource);
        }
    }
}
