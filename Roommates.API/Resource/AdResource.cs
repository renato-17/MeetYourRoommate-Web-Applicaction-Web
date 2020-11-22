using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class AdResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PropertyResource Property { get; set; }
    }
}
