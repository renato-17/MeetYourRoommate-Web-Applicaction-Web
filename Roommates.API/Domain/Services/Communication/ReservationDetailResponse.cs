using Roommates.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Services.Communication
{
    public class ReservationDetailResponse : BaseResponse<ReservationDetail>
    {
        public ReservationDetailResponse(ReservationDetail resource) : base(resource)
        {
        }

        public ReservationDetailResponse(string message) : base(message)
        {
        }

        public ReservationDetailResponse(ReservationDetail resource, string message) : base(resource, message)
        {
        }
    }
}
