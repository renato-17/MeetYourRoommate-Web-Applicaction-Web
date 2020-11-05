using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class PropertyDetail
    {
        public int Id { get; set; }
        public int SquareMeters { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int Kitchen { get; set; }
        public int Livingroom { get; set; }
        public int Price { get; set; }

        //Relation with Property Detail
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        //Relation with Property Resource
        public IList<PropertyResource> PropertyResources { get; set; } = new List<PropertyResource>();

    }
}
