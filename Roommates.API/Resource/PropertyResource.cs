﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class PropertyResource
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public LessorResource Lessor { get; set; }
    }
}
