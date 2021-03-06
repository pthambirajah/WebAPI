﻿using System;
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
    public class FlightsController : ControllerBase
    {
        private readonly TodoContext _context;

        public FlightsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            //Get flights and corresponding bookings 
            var b = await _context.FlightSet.Include(x => x.BookingSet).ToListAsync();
           
            //This is used to stop from escalating
            //booking contains flight which contains bookings which contains flight...
            foreach (Flight f in b)
            {
               foreach(Booking b2 in f.BookingSet)
                {
                    b2.Flight = null;
                }
            }

            //not full flights only
            List<Flight> flights = new List<Flight>();
            foreach (Flight flight in b)
            {
                int numberOfBookings = 0;
                foreach (Booking book in flight.BookingSet)
                {
                    numberOfBookings++;
                }
                if (numberOfBookings < flight.Seats)
                {
                    flights.Add(flight);
                }
            }
            return flights;
        }

        // GET: api/Flights/5
        //Default method to access a flight
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // GET: api/Flights/5
        [HttpGet("Price/{id}")]
        public double GetFlightPrice(int id)
        {
           

            //Get the flight details
            var flight =  _context.FlightSet.Find(id);
            //In case the flight doesn't exist
            if (flight == null)
            {
                return 9999999;
            }

            int NumberOfBookings = 0;

            //Get all bookings corresponding to this flight
            var bookings = from b in _context.BookingSet
                           where b.FlightNo == flight.FlightNo
                           select b;
            //Count number of bookings
            foreach (Booking book in bookings)
            {
                NumberOfBookings++;
            }
            //Calculate the number of seats still available on the flight
            double AvailableSeats = (double)flight.Seats - NumberOfBookings;

            //Calculates how full is the flight
            //double FillRate = (double)flight.AvailableSeats / (double)flight.Seats;
            double FillRate = AvailableSeats / (double)flight.Seats;

            //If the airplane is more than 80% full regardless of the date:
            if (FillRate < 0.2d)
             {
                return (double)flight.BasePrice * 1.5;
             }
             
             //calculates number of dates between departure and today
             int days = (flight.Date - DateTime.Now).Days;

            //If the plane is filled less than 20% less than 2 months before departure
            if (FillRate > 0.8 && days < 60)
             {
                return (double)flight.BasePrice * 0.8; 
             }

            //If the plane is filled less than 50% less than 1 month before departure
            if (FillRate > 0.5 && days < 30)
             {
                return (double)flight.BasePrice * 0.7;
             }

            //If none of those conditions are filled, we return the original price.
            return (double)flight.BasePrice;
      
        }

        // PUT: api/Flights/5
        //Default method to update a flight
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //Default method to add a flight
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            _context.FlightSet.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.FlightNo }, flight);
        }

        // DELETE: api/Flights/5
        //Default method to delete a flight
        [HttpDelete("{id}")]
        public async Task<ActionResult<Flight>> DeleteFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.FlightSet.Remove(flight);
            await _context.SaveChangesAsync();

            return flight;
        }

        //Default method to check if the flight exist
        private bool FlightExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightNo == id);
        }
    }
}
