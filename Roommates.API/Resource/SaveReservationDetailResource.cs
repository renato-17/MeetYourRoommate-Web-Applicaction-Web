using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SaveReservationDetailResource
    { 

        [Required]
        public int Amount { get; set; }
        [Required]
        public int Downpayment { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int PropertyId { get; set; }
    }
}
