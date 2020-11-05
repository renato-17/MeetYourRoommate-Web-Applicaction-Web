using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool Close { get; set; }

        //Relation with Lessor
        public int LessorId { get; set; }
        public Lessor Lessor { get; set; }

        //Relation with Property
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        //Relation with Comment
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
