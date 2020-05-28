using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public class Booking
    {
        // flight - passenger
        public int FlightNo { get; set; }
        public int PassengerID { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}
