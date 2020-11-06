using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class ReservationResponse: BaseResponse<Reservation>
    {
        public ReservationResponse(Reservation resource) : base(resource)
        {
        }

        public ReservationResponse(string message) : base(message)
        {
        }

        public ReservationResponse(Reservation resource, string message) : base(resource, message)
        {
        }
    }
}
