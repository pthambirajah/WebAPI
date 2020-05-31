using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITuto.Models
{
    [NotMapped]
    class Boeing : Flight
    {
        [Key]
        public int _flightNo;
        [StringLength(50), MinLength(3)]
        private string _departure;
        [StringLength(50), MinLength(3)]
        private string _destination;
        private DateTime _date;
        private short? _seats;
        private short? _availableSeats;
        [Required]
        private short? _basePrice;
        /*private virtual ICollection<Booking> _bookingSet;*/
        public Boeing( string departure, string destination, DateTime date, short? basePrice)
        {

            _departure = departure;
            _destination = destination;
            _date = date;
            _seats = 350;
            _basePrice = basePrice;
        }

        public override int FlightNo
        {
            get { return _flightNo; }
            set { _flightNo = value; }
        }

        public override string Departure
        {
            get { return _departure; }
            set { _departure = value; }
        }
        public override string Destination
        {
            get { return _departure; }
            set { _departure = value; }
        }

        public override DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public override short? Seats
        {
            get { return _seats; }
        }
        public override short? AvailableSeats
        {
            get { return _availableSeats; }
            set { _availableSeats = value; }
        }
        public override short? BasePrice 
        {
            get { return _basePrice; }
            set { _basePrice = value; }
        }

        /*public ICollection<Booking> BookingSet
        {
            get { return _bookingSet; }
            set { _bookingSet = value; }
        }*/

    }
}
