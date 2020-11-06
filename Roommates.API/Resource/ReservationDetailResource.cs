using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class ReservationDetailResource
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Downpayment { get; set; }
        public int StudentId { get; set; }
        public int LessorId { get; set; }
        public int PropertyId { get; set; }
        public int ReservationId { get; set; }

    }
}
