﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class JetFactory : FlightFactory
    {

        private string Departure;
        private string Destination;
        private DateTime Date;
        private short? BasePrice;

        public JetFactory(string departure, string destination, DateTime date, short? basePrice)
        {

            Departure = departure;
            Destination = destination;
            Date = date;
            BasePrice = basePrice;
        }

        public override Flight GetFlight()
        {
            return new Jet(Departure, Destination, Date, BasePrice);
        }

    }
}
