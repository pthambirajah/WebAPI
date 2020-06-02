using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class BoeingFactory : FlightFactory
    {
        private string _departure;
        private string _destination;
        private DateTime _date;
        private short? _seats;
        private short? _basePrice;
        private State _state = null;

        /*public BoeingFactory(string departure, string destination, DateTime date, short? basePrice)
        {
            _departure = departure;
            _destination = destination;
            _date = date;
            _seats = 350;
            _basePrice = basePrice;
        }*/

        public BoeingFactory(State state)
        {
            _state = state;
        }

        public override Flight CreateFlight()
        {
            return new Boeing(_state);
        }

    }
}
