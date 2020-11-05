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

 
    }
}