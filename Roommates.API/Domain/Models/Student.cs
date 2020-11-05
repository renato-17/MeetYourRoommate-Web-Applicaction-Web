using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Student : Person
    {
        public string Description { get; set; }
        public string Hobbies { get; set; }
        public bool Smoker { get; set; }

        // Relation with Team
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        //Relation with Campus
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
    }
}