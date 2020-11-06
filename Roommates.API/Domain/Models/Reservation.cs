using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;

namespace Roommates.API.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        //Relation with Reservation Detail
        public IList<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();
    }
}
