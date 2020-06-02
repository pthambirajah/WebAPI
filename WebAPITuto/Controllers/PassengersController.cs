using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITuto.Models;

namespace WebAPITuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly TodoContext _context;

        public PassengersController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Passengers
        //Default method to get all passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers()
        {
            return await _context.Passengers.ToListAsync();
        }

        // GET: api/Passengers
        //Get the last passenger that has been added
        [HttpGet("Last/")]
        public string GetPassengerID()
        {
            var result = from p in _context.Passengers
                         select p;
            int passengerID = 0;
            foreach(Passenger passenger in result)
            {
                if(passenger.PersonID > passengerID)
                {
                    passengerID = passenger.PersonID;
                }
            }
            return passengerID.ToString();
        }

        // GET: api/Passengers/5
        //Default method to access one passenger
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //Default method to update one passenger
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.PersonID)
            {
                return BadRequest();
            }

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Passengers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //Default method to add one passenger
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = passenger.PersonID }, passenger);
        }

        // DELETE: api/Passengers/5
        //Default method to delete one passenger
        [HttpDelete("{id}")]
        public async Task<ActionResult<Passenger>> DeletePassenger(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();

            return passenger;
        }

        //Default method to check if a passenger exists
        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.PersonID == id);
        }
    }
}
