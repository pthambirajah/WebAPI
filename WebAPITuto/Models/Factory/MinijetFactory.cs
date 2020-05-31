using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class MinijetFactory : FlightFactory
    {

        private string Departure;
        private string Destination;
        private DateTime Date;
        private short? BasePrice;

        public MinijetFactory(string departure, string destination, DateTime date, short? basePrice)
        {

            Departure = departure;
            Destination = destination;
            Date = date;
            BasePrice = basePrice;
        }

        public override Flight GetFlight()
        {
            return new Minijet(Departure, Destination, Date, BasePrice);
        }

    }
}
