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
    public class StatisticsController : ControllerBase
    {
        private readonly TodoContext _context;

        public StatisticsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Statistics
        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetFlightSet()
        {
            
            List<Flight> flights = new List<Flight>();
            foreach (Flight flight in _context.FlightSet)
            {
                if (flight.AvailableSeats > 0)
                {
                    flights.Add(flight);
                }
            }
                return flights;
        }

        // GET: api/Statistics/5
        [HttpGet("AveragePrice/{destination}")]
        public double GetDestinationAveragePrice(string Destination)
        {
            var flight = from f in _context.FlightSet
                         where f.Destination.Equals(Destination)
                         select f;

            double TotalSalePrice = 0;
            int NumberOfBookings = 0;
           foreach(Flight f in flight)
            {
                var bookings = from b in _context.BookingSet
                               where b.FlightNo == f.FlightNo
                               select b;
                foreach(Booking book in bookings)
                {
                    TotalSalePrice += book.SalePrice;
                    NumberOfBookings++;
                }
            }

            double AveragePrice = TotalSalePrice / NumberOfBookings;
            return AveragePrice;
        }

        // Get: api/Statistics/TotalSales/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpGet("TotalSales/{id}")]
        public double GetFlightTotalSales(int id)
        {
            var flight = _context.FlightSet.Find(id);

            if (flight == null)
            {
                return 0;
            }

            var bookings = from b in _context.BookingSet
                           where b.FlightNo == id
                           select b;

            double TotalSales=0;
            foreach(Booking price in bookings)
            {
                TotalSales += price.SalePrice;
            }
            return TotalSales;


        }

        // POST: api/Statistics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            _context.FlightSet.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.FlightNo }, flight);
        }

        // DELETE: api/Statistics/5
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

        private bool FlightExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightNo == id);
        }
    }
}
