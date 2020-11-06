﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        // Relation with Lessor
        public int LessorId { get; set; }
        public Lessor Lessor { get; set; }
        
        //Relation with Property Detail
        public PropertyDetail PropertyDetail { get; set; }

        //Relation with Ads
        public IList<Ad> Ads { get; set; } = new List<Ad>();

        //Relation with Reservation Detail
        public IList<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    }
}
