using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relation with Student
        public IList<Student> Students { get; set; } = new List<Student>();

        //Relation with Task
        public IList<Task> Tasks { get; set; } = new List<Task>();
    }
}
