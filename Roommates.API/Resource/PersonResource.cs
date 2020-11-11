using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class PersonResource
    {
        public int Id { get; set; }
        public string Discriminator { get; set; }
    }
}
