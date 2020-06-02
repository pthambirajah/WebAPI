using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public class Booking
    {
        public int FlightNo { get; set; }
        public int PersonID { get; set; }
        //Final price that the passenger paid to get this ticket
        public double SalePrice { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}
