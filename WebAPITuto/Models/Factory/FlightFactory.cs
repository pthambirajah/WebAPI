using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    //public abstract class FlightFactory
    abstract class FlightFactory
    {
        public abstract Flight CreateFlight();

    }
}
