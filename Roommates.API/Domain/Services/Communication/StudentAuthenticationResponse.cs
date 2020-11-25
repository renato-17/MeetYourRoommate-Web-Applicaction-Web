using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class StudentAuthenticationResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public DateTime Birthdate { get; set; }
        public string Description { get; set; }
        public string Hobbies { get; set; }
        public bool Smoker { get; set; }

        public string Token { get; set; }

        public StudentAuthenticationResponse(Student student, string token)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Dni = student.Dni;
            Phone = student.Phone;
            Gender = student.Gender;
            Address = student.Address;
            Mail = student.Mail;
            Birthdate = student.Birthdate;
            Description = student.Description;
            Smoker = student.Smoker;
            Token = token;
        }
       
    }
}
