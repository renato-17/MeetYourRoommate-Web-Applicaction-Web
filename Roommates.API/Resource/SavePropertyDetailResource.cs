using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Resource
{
    public class SavePropertyDetailResource
    {
        public int SquareMeters { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int Kitchen { get; set; }
        public int Livingroom { get; set; }
        public int Price { get; set; }
    }
}
