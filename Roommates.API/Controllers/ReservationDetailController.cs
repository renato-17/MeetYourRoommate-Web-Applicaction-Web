using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain.Models;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Produces("application/json")]
    [Route("api/reservation/{reservationId}/[controller]")]
    [ApiController]
    public class ReservationDetailController : ControllerBase
    {
        private readonly IReservationDetailService _reservationDetailService;
        private readonly IMapper _mapper;

        public ReservationDetailController(IReservationDetailService reservationDetailService, IMapper mapper)
        {
            _reservationDetailService = reservationDetailService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "Get all Reservation Details by Reservation",
           Description = "Get all Reservation Details by an specific Reservation",
           OperationId = "GetAllReservationDetailsByReservationId",
           Tags = new[] { "reservation_details" }
           )]
        [HttpGet]
        public async Task<IEnumerable<Resource.ReservationDetailResource>> GetAllByReservationIdAsync(int reservationId)
        {
            var properties = await _reservationDetailService.ListByReservationIdAsync(reservationId);
            var resource = _mapper.Map<IEnumerable<ReservationDetail>, IEnumerable<Resource.ReservationDetailResource>>(properties);
            return resource;
        }

        [SwaggerOperation(
           Summary = "Get Reservation Details by Id and Reservation",
           Description = "Get Reservation Details by its id and Reservation",
           OperationId = "GetReservationDetailsByIdAndReservationId",
           Tags = new[] { "reservation_details" }
           )]
        [HttpGet("{id}")]
        public async Task<Resource.ReservationDetailResource> GetByReservationDetailIdAndReservationId(int id, int reservationId)
        {
            var reservationDetail = await _reservationDetailService.GetByIdAndReservationIdAsync(id, reservationId);
            var resource = _mapper.Map<ReservationDetail, Resource.ReservationDetailResource>(reservationDetail.Resource);
            return resource;
        }

        [SwaggerOperation(
            Summary = "Create Reservation Details",
            Description = "Create a new Reservation Details",
            OperationId = "CreateReservationDetail",
            Tags = new[] { "reservation_details" }
            )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveReservationDetailResource resource, int reservationId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var reservationDetail = _mapper.Map<SaveReservationDetailResource, ReservationDetail>(resource);

            var result = await _reservationDetailService.SaveAsync(reservationDetail,reservationId,resource.StudentId,resource.PropertyId);

            if (!result.Success)
                return BadRequest(result.Message);

            var reservationDetailResource = _mapper.Map<ReservationDetail, ReservationDetailResource>(result.Resource);

            return Ok(reservationDetailResource);
        }

        [SwaggerOperation(
            Summary = "Update Reservation Details",
            Description = "Update an specific Reservation Details",
            OperationId = "UpdateReservationDetail",
            Tags = new[] { "reservation_details" }
            )]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveReservationDetailResource resource,int id, int reservationId)
        {
            var reservationDetail = _mapper.Map<SaveReservationDetailResource, ReservationDetail>(resource);

            var result = await _reservationDetailService.UpdateAsync(reservationDetail,id, reservationId);
            if (!result.Success)
                return BadRequest(result.Message);

            var reservationDetailResource = _mapper.Map<ReservationDetail, Resource.ReservationDetailResource>(result.Resource);
            return Ok(reservationDetailResource);
        }

        [SwaggerOperation(
            Summary = "Delete Reservation Details",
            Description = "Delete an specific Reservation Details",
            OperationId = "DeleteReservationDetail",
            Tags = new[] { "reservation_details" }
            )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, int reservationId)
        {
            var result = await _reservationDetailService.DeleteAsync(id,reservationId);
            if (!result.Success)
                return BadRequest(result.Message);
            var reservationDetailResource = _mapper.Map<ReservationDetail, Resource.ReservationDetailResource>(result.Resource);
            return Ok(reservationDetailResource);
        }
    }
}
