using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class MinijetFactory : FlightFactory
    {

        private string _departure;
        private string _destination;
        private DateTime _date;
        private short? _basePrice;
        private short? _seats;
        private State _state = null;

        public MinijetFactory(string departure, string destination, DateTime date, short? basePrice)
        {

            _departure = departure;
            _destination = destination;
            _date = date;
            _basePrice = basePrice;
        }
        
        public override Flight CreateFlight()
        {
            return new Minijet(_departure, _destination, _date, _basePrice, 15);
        }
    }
}
