using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveLessorResource
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        public string Dni { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(9)]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Mail { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(40)]
        public string Password { get; set; }
        
        public bool Premium { get; set; }
    }
}
