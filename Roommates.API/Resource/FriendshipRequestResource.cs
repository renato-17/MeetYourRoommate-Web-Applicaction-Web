using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class FriendshipRequestResource
    {
        public int StudentOneId { get; set; }
        public int StudentTwoId { get; set; }
        public string StatusDetail { get; set; }
    }
}
