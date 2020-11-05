using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class PropertyResource
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime DateUpload { get; set; }


        //Relation with Property Detail
        public int PropertyDetailId { get; set; }
        public PropertyDetail PropertyDetail { get; set; }
    }

}
