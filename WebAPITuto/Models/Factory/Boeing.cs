using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPITuto.Models.Factory;

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
        private short? _seats = 350;
        [Required]
        private short? _basePrice;
        public virtual ICollection<Booking> BookingSet { get; set; }

        //State Pattern, this class is our context
        //This is a reference to the current state of the flight
        private State _state = null;

        public Boeing( string departure, string destination, DateTime date, short? basePrice, short? seats)
        {

            _departure = departure;
            _destination = destination;
            _date = date;
            _seats = seats;
            _basePrice = basePrice;

            //As soon as we have all the information, flight tickets could be sold.
            this.TransitionTo(new SaleState());
        }
        public void OpenSales(string departure, string destination, DateTime date, short? basePrice)
        {

            _departure = departure;
            _destination = destination;
            _date = date;
            
            _basePrice = basePrice;

            //As soon as we have all the information, flight tickets could be sold.
            this.TransitionTo(new SaleState());
        }

        public Boeing(State state)
        {
            this.TransitionTo(state);
        }
        //This methods allows us to change the state.
        public void TransitionTo(State state)
        {
            Console.WriteLine("Transition to : " + state);
            this._state = state;
            this._state.SetContext(this);
        }

        //This method returns the behaviour that the flight should have depending on its state.
        //Which means, if we are able to buy tickets.
        public string CanBuyTickets()
        {
            return this._state.BuyTickets();
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
            get { return _destination; }
            set { _destination = value; }
        }

        public override DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public override short? Seats
        {
            get { return _seats; }
            set { _seats = value; }
        }
        public override short? BasePrice
        {
            get { return _basePrice; }
            set { _basePrice = value; }
        }
    }
}
