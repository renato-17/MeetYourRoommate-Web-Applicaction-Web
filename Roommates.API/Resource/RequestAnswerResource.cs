using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class RequestAnswerResource
    {
        [Required]
        public int Answer { get; set; }
    }
}
