using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITuto.Models;

namespace WebAPITuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly TodoContext _context;

        public BookingsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingSet()
        {
            var f =  await _context.BookingSet.Include(x => x.Flight).Include(x => x.Passenger).ToListAsync();

            //This is used to stop from escalating
            //booking contains flight which contains bookings which contains flight...
            foreach(Booking b in f)
            {
                b.Flight.BookingSet = null;
                b.Passenger.BookingSet = null;
            }
            return f;
        }

        
        // GET: api/Bookings/Destination/destination
        [HttpGet("Destination/{destination}")]
        public List<Ticket> GetAllBookingsDestination(string Destination)
        {
            var flight = from f in _context.FlightSet
                         where f.Destination.Equals(Destination)
                         select f;
            
            var FinalTickets = new List<Ticket>();

         foreach (Boeing f in flight)
            {
                var bookings = (from b in _context.BookingSet
                                where b.FlightNo == f.FlightNo
                                select new Ticket { FlightNo = b.FlightNo, GivenName = b.Passenger.GivenName, Surname = b.Passenger.Surname, SalePrice = b.SalePrice }).ToList();
                foreach (Ticket ticket in bookings)
                {
                    FinalTickets.Add(ticket);
                }
            }
            return FinalTickets;
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.BookingSet.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            
            _context.BookingSet.Add(booking);
            
           try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.FlightNo))
                {
                    if (BookingExists(booking.PersonID))
                    {
                        return Conflict();
                    }
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBooking", new { id = booking.FlightNo }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.BookingSet.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.BookingSet.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        private bool BookingExists(int id)
        {
            return _context.BookingSet.Any(e => e.FlightNo == id);
        }
    }
}
