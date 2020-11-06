using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Lessor:Person
    {
        public bool Premium { get; set; }

        //Relation with Properties
        public IList<Property> Properties { get; set; } = new List<Property>();

        //Relation with Ads
        public IList<Ad> Ads { get; set; } = new List<Ad>();

        //Relation with Reservation Detail
        public IList<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();
    }
}
