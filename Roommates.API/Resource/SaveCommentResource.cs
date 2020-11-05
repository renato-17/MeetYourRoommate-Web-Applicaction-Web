using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveCommentResource
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

    }
}
