using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveTeamResource
    {
    
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
