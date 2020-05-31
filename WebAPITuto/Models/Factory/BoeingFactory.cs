using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    public class BoeingFactory : FlightFactory
    {
        private string _departure;
        private string _destination;
        private DateTime _date;
        private short? _seats;
        private short? _availableSeats;
        private short? _basePrice;

        public BoeingFactory(string departure, string destination, DateTime date, short? basePrice)
        {
            _departure = departure;
            _destination = destination;
            _date = date;
            _basePrice = basePrice;
        }

        public override Flight GetFlight()
        {
            return new Boeing(_departure, _destination, _date, _basePrice);
        }
    }
}
