using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SavePropertyDetailResource
    {
        [Required]
        public int SquareMeters { get; set; }
        [Required]
        public int Rooms { get; set; }
        [Required]
        public int Bathrooms { get; set; }
        [Required]
        public int Kitchen { get; set; }
        [Required]
        public int Livingroom { get; set; }
        
        [Required]
        public int Price { get; set; }

    }
}
