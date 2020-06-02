using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public class Ticket
    {
        //This one has been created to simplify the list of bookings in one row.
        public int FlightNo { get; set; }
         
        public string Surname { get; set; }

        public string GivenName { get; set; }

        public double SalePrice { get; set; }
    }
}
