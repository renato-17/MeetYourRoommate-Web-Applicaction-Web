﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveAdResource
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public int LessorId { get; set; }
        [Required]
        public int PropertyId { get; set; }
    }
}
