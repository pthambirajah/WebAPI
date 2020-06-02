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
        //Get all available flights
        //Exists also in flight controller
        //Get only flights with available seats
        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetFlightSet()
        {
            
            List<Flight> flights = new List<Flight>();

            foreach (Flight flight in _context.FlightSet)
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

        // GET: api/Statistics/5
        //Calculate the average price of the tickets we sold for a destination
        [HttpGet("AveragePrice/{destination}")]
        public double GetDestinationAveragePrice(string Destination)
        {
            //Get all the flights for the given destination
            var flight = from f in _context.FlightSet
                         where f.Destination.Equals(Destination)
                         select f;

            double TotalSalePrice = 0;
            int NumberOfBookings = 0;

            //Double loop, sum up sale price from each bookings in each flights
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
            //Total turnover divided by the number of bookings
            double AveragePrice = TotalSalePrice / NumberOfBookings;
            return AveragePrice;
        }

        // Get: api/Statistics/TotalSales/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //Get total turnover of a flight
        [HttpGet("TotalSales/{id}")]
        public double GetFlightTotalSales(int id)
        {
            var flight = _context.FlightSet.Find(id);
            //Return 0 if the flight doesn't exist
            if (flight == null)
            {
                return 0;
            }
            //Get all the bookings of this flights
            var bookings = from b in _context.BookingSet
                           where b.FlightNo == id
                           select b;

            double TotalSales=0;
            //Sum up the sale price and return the result
            foreach(Booking price in bookings)
            {
                TotalSales += price.SalePrice;
            }
            return TotalSales;


        }

    }
}
