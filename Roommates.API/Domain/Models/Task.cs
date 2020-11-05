using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishDate { get; set;  }
        public bool Active { get; set; }

        //Relation with Team
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
