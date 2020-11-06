using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class FriendshipRequestResource
    {
        public int PersonOneId { get; set; }
        public int PersonTwoId { get; set; }
        public string StatusDetail { get; set; }
        public string Type { get; set; }
    }
}
