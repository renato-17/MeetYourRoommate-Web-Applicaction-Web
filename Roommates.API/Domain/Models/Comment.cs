using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Likes { get; set; }

        //Relation with Person
        public int PersonId { get; set; }
        public Person Person { get; set; }

        //Relation with Ad
        public int AdId { get; set; }
        public Ad Ad { get; set; }

    }
}
