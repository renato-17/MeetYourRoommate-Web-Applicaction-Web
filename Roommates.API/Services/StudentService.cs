﻿using Microsoft.VisualBasic;
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
        public readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
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
   

            try
            {
                await _studentRepository.AddAsync(student);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(student);
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return new StudentResponse($"An error ocurred while removing student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> UpdateAsync(int id, Student student)
        {
            var existingStudent = await _studentRepository.FindById(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found");

            existingStudent = UpdatedStudent(student);
             
            try
            {
                _studentRepository.Update(existingStudent);
                await _unitOfWork.CompleteAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error ocurred while removing student: {ex.Message}");
            }
        }
      

        private Student UpdatedStudent(Student student)
        {
           
            return new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Dni = student.Dni,
                Phone = student.Phone,
                Gender = student.Gender,
                Address = student.Address,
                Birthdate = student.Birthdate,
                Description = student.Description,
                Hobbies = student.Hobbies,
                Smoker = student.Smoker
            };
        }

        
    }
}