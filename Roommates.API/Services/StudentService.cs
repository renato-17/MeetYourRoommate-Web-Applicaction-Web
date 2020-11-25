using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Repositories;
using Roommates.API.Domain.Services;
using Roommates.API.Domain.Services.Communication;
using Roommates.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppSettings _appSettings;
        private readonly IPersonRepository _personRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly ITeamRepository _teamRepository;
        public readonly IUnitOfWork _unitOfWork;


        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ITeamRepository teamRepository, ICampusRepository campusRepository,
            IOptions<AppSettings> appSettings, IPersonRepository personRepository)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _teamRepository = teamRepository;
            _campusRepository = campusRepository;
            _appSettings = appSettings.Value;
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsByTeamId(int teamId)
        {
            return await _studentRepository.ListByTeamIdAsync(teamId);

        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _studentRepository.ListAsync();
        }


        public async Task<StudentResponse> GetByIdAsync(int id)
        {
            var existingStudent = await _studentRepository.FindById(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found");

            return new StudentResponse(existingStudent);
        }


        public async Task<StudentResponse> SaveAsync(Student student, int studyCenterId)
        {
            var person = await _personRepository.FindByDni(student.Dni);
            if (person != null)
                return new StudentResponse("Dni already exists");

            person = await _personRepository.FindByMail(student.Mail);
            if (person != null)
                return new StudentResponse("Mail already exists");

            person = await _personRepository.FindByPhone(student.Phone);
            if (person != null)
                return new StudentResponse("Phone already exists");

            var existingCampus = await _campusRepository.FindByIdAndStudyCenterId(studyCenterId, student.CampusId);
            if (existingCampus == null)
                return new StudentResponse("Study Center or Campus not found");

            student.Campus = existingCampus;
            student.Available = true;

            try
            {
                await _studentRepository.AddAsync(student);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(student);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while saving student: {ex.InnerException}");
            }

        }
        
        public async Task<StudentResponse> RemoveAsync(int id)
        {
            var existingStudent = await _studentRepository.FindById(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found");

            try
            {
                _studentRepository.Remove(existingStudent);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while removing student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> UpdateAsync(int id, Student student)
        {
            var existingStudent = await _studentRepository.FindById(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found");

           
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Dni = student.Dni;
            existingStudent.Phone = student.Phone;
            existingStudent.Gender = student.Gender;
            existingStudent.Address = student.Address;
            existingStudent.Birthdate = student.Birthdate;
            existingStudent.Description = student.Description;
            existingStudent.Hobbies = student.Hobbies;
            existingStudent.Smoker = student.Smoker;
            existingStudent.Mail = student.Mail;
            existingStudent.Password = student.Password;
            existingStudent.Available = student.Available;

            try
            {
                _studentRepository.Update(existingStudent);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while update student: {ex.InnerException}");
            }
        }

        public async Task<StudentResponse> JoinTeam(Team team, int id)
        {
            var student = GetByIdAsync(id).Result.Resource;

            if (!student.Available)
                return new StudentResponse("You should be available to join a team");

            if (student.Team != null)
                return new StudentResponse("Student already have team");

            var existingTeam = await _teamRepository.FindByName(team.Name);

            string message;

            if (existingTeam != null)
            {
                student.Team = existingTeam;
                message = "Join the team succesfully";
            }
            else
            {
                student.Team = team;
                message = "Join to a new team succesfully";
            }

            try
            {
                _studentRepository.Update(student);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(student, message);

            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while join a team: {ex.Message}");
            }

        }

        public async Task<StudentResponse> LeaveTeam(int id)
        {
            var existingStudent = await _studentRepository.FindById(id);
            var team = existingStudent.Team;

            if (existingStudent == null)
                return new StudentResponse("Student not found");

            if (existingStudent.Team == null)
                return new StudentResponse("Student does not have team");

            existingStudent.Team = null;
            existingStudent.TeamId = null;


            var students = _studentRepository.ListByTeamIdAsync(team.Id).Result.ToList();

            try
            {
                _studentRepository.Update(existingStudent);

                if (students.Count == 1)
                    _teamRepository.Remove(team);

                await _unitOfWork.CompleteAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while leaving a team: {ex.Message}");
            }

        }

        public async Task<StudentAuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var students = await ListAsync();

            var student = students.SingleOrDefault(s =>
                s.Mail == request.Mail &&
                s.Password == request.Password
                );

            if (student == null) return null;

            var token = GenerateJwtToken(student);  
            return new StudentAuthenticationResponse(student, token);
        }

        private string GenerateJwtToken(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, student.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
