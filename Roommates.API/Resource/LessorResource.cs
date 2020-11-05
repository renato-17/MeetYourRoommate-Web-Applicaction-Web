using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class LessorResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public bool Premium { get; set; }
    }
}
