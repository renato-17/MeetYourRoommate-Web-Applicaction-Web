using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class CommentResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
    }
}
