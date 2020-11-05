using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveStudentResource
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(8)]
        public string Dni { get; set; }
        [Required]
        [MaxLength(13)]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [MaxLength(150)]
        public string Hobbies { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Mail { get; set; }
        public bool Smoker { get; set; }
        
        [Required]
        public int StudyCenterId { get; set; }

        [Required]
        public int CampusId { get; set; }

    }
}
