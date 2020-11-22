using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Domain;
using Roommates.API.Domain.Services;
using Roommates.API.Extensions;
using Roommates.API.Resource;
using Swashbuckle.AspNetCore.Annotations;

namespace Roommates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [SwaggerOperation(
          Summary = "Get all Reservations",
          Description = "Get all Reservations",
          OperationId = "GetAllReservation",
          Tags = new[] { "reservations" }
          )]
        [HttpGet]
        public async Task<IEnumerable<ReservationResource>> GetAllAsync()
        {
            var reservations = await _reservationService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResource>>(reservations);
            return resource;
        }

        [SwaggerOperation(
          Summary = "Get Reservation",
          Description = "Get an specific Reservation by its id",
          OperationId = "GetReservationById",
          Tags = new[] { "reservations" }
          )]
        [HttpGet("{id}")]
        public async Task<ReservationResource> GetByReservationId(int id)
        {
            var reservations = await _reservationService.GetByIdAsync(id);
            var resource = _mapper.Map<Reservation, ReservationResource>(reservations.Resource);
            return resource;
        }

        [SwaggerOperation(
          Summary = "Create Reservation",
          Description = "Create a new Reservation",
          OperationId = "CreateReservation",
          Tags = new[] { "reservations" }
          )]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveReservationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetMessages());

            var reservations = _mapper.Map<SaveReservationResource, Reservation>(resource);
            var result = await _reservationService.SaveAsync(resource.StudentId,resource.PropertyId,reservations);

            if (!result.Success)
                return BadRequest(result.Message);

            var reservationResource = _mapper.Map<Reservation, ReservationResource>(reservations);
            return Ok(reservationResource);
        }

        [SwaggerOperation(
          Summary = "Delete Reservation",
          Description = "Delete an specific Reservation",
          OperationId = "DeleteReservation",
          Tags = new[] { "reservations" }
          )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reservationService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var reservationResource = _mapper.Map<Reservation, ReservationResource>(result.Resource);

            return Ok(reservationResource);
        }

        [SwaggerOperation(
          Summary = "Update Reservation",
          Description = "Update an specific Reservation",
          OperationId = "UpdateReservation",
          Tags = new[] { "reservations" }
          )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveReservationResource resource, int id)
        {
            var reservation = _mapper.Map<SaveReservationResource, Reservation>(resource);
            var result = await _reservationService.UpdateAsync(id, reservation);

            if (!result.Success)
                return BadRequest(result.Message);

            var reservationsResource = _mapper.Map<Reservation, ReservationResource>(result.Resource);

            return Ok(reservationsResource);
        }
    }
}
