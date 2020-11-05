using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Campus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        //Relation with StudyCenter
        public int StudyCenterId { get; set; }
        public StudyCenter StudyCenter { get; set; }

        //Relation with Student
        public IList<Student> Students { get; set; } = new List<Student>();
    }
}
