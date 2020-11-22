using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Request
    {
        public int Status { get; set; }
        public string StatusDetail { get; set; }
        public string Type { get; set; }

        //Relation with the first Student
        public int PersonOneId { get; set; }
        public Person PersonOne { get; set; }

        //Relation with the second Student
        public int PersonTwoId { get; set; }
        public Person PersonTwo { get; set; }

        public int PropertyId { get; set; }
    }
}
