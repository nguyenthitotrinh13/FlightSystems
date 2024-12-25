using FlightMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using FlightMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightMicroservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var flights = _flightRepository.GetFlights();
            return new OkObjectResult(flights);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var flights = _flightRepository.GetFlightByID(id);
            return new OkObjectResult(flights);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Flight flight)
        {
            using (var scope = new TransactionScope())
            {
                _flightRepository.InsertFlight(flight);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = flight.FlightID }, flight);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] Flight flight)
        {
            if(flight != null)
            {
                using (var scope = new TransactionScope())
                {
                    _flightRepository.UpdateFlight(flight);
                    scope.Complete();
                    return new OkResult();
                }
            }
           return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _flightRepository.DeleteFlight(id);
            return new OkResult();
        }
    }
}
