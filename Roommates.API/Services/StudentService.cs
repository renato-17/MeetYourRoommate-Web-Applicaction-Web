using Microsoft.VisualBasic;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly ITeamRepository _teamRepository;
        public readonly IUnitOfWork _unitOfWork;


        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ITeamRepository teamRepository, ICampusRepository campusRepository)

        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _teamRepository = teamRepository;
            _campusRepository = campusRepository;

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
            var existingCampus = await _campusRepository.FindByIdAndStudyCenterId(studyCenterId, student.CampusId);
            if (existingCampus == null)
                return new StudentResponse("Study Center or Campus not found");

            student.Campus = existingCampus;

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

            if (student.Team != null)
                return new StudentResponse(student);

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


    }
}
