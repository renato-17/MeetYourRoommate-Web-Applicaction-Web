using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class FriendshipRequest 
    {
        public int Status { get; set; }
        public String StatusDetail { get; set; }

        //Relation with the first Student
        public int StudentOneId { get; set; }
        public Student StudentOne { get; set; }

        //Relation with the second Student
        public int StudentTwoId { get; set; }
        public Student StudentTwo { get; set; }
    }
}
