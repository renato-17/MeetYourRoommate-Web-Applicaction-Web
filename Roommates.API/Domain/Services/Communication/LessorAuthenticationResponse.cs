using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class LessorAuthenticationResponse
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
        public bool Premium { get; set; }

        public string Token { get; set; }

        public LessorAuthenticationResponse(Lessor lessor, string token)
        {
            Id = lessor.Id;
            FirstName = lessor.FirstName;
            LastName = lessor.LastName;
            Dni = lessor.Dni;
            Phone = lessor.Phone;
            Gender = lessor.Gender;
            Address = lessor.Address;
            Mail = lessor.Mail;
            Birthdate = lessor.Birthdate;
            Premium = lessor.Premium;
            Token = token;
        }
    }
}
