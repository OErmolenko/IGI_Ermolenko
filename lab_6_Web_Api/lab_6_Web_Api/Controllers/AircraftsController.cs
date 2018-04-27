using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab_6_Web_Api.Models;
using lab_6_Web_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace lab_6_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class AircraftsController : Controller
    {
        readonly AirportContext _context;

        public AircraftsController(AirportContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public List<AircraftViewModels> Get()
        {
            var list = _context.Aircrafts.Include(t => t.Type).Select(o =>
              new AircraftViewModels
              {
                  AircraftId = o.AircraftId,
                  Mark = o.Mark,
                  Capasity = o.Capasity,
                  Carrying = o.Carrying,
                  TypeId = o.TypeId,
                  NameType = o.Type.NameType
              });
            return list.OrderBy(t => t.AircraftId).ToList();
        }

        // GET api/values
        [HttpGet("types")]
        [Produces("application/json")]
        public IEnumerable<Models.Type> GetFuels()
        {
            return _context.Types.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Aircraft aircraft = _context.Aircrafts.FirstOrDefault(o => o.AircraftId == id);
            if (aircraft == null)
                return NotFound();
            return new ObjectResult(aircraft);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Aircraft aircraft)
        {
            if (aircraft == null)
            {
                return BadRequest();
            }

            _context.Aircrafts.Add(aircraft);
            _context.SaveChanges();
            return Ok(aircraft);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Aircraft aircraft)
        {
            if (aircraft == null)
            {
                return BadRequest();
            }

            if(!_context.Aircrafts.Any(o => o.AircraftId == aircraft.AircraftId))
            {
                return NotFound();
            }

            _context.Update(aircraft);
            _context.SaveChanges();

            return Ok(aircraft);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Aircraft aircraft = _context.Aircrafts.FirstOrDefault(o => o.AircraftId == id);

            if(aircraft == null)
            {
                return NotFound();
            }

            var flights = _context.Flights.Where(t => t.AircraftId == aircraft.AircraftId);
            foreach (var flight in flights)
            {
                var tickets = _context.Tickets.Where(t => t.FlightId == flight.FlightId);
                _context.Tickets.RemoveRange(tickets);
                _context.SaveChanges();
            }
            _context.Flights.RemoveRange(flights);
            _context.SaveChanges();
            _context.Aircrafts.Remove(aircraft);
            _context.SaveChanges();

            return Ok(aircraft);
        }
    }
}
