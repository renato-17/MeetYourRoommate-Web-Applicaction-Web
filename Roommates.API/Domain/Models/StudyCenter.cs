using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class StudyCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relation with Campus
        public IList<Campus> Campus { get; set; } = new List<Campus>();

    }
}
