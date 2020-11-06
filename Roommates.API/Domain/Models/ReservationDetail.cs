using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class ReservationDetail
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Downpayment { get; set; }

        // Relation with Student
        public int StudentId { get; set; }
        public Student Student { get; set; }

        //Relation with Lessor
        public int LessorId { get; set; }
        public Lessor Lessor { get; set; }

        //Relation with Property
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        //Relation with Reservation
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
