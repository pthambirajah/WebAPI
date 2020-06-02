using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    //public abstract class Flight
    public abstract class Flight
    {
       //As it is possible to have many different flights, we implemented a factory pattern for it.
        [Key]
        public abstract int FlightNo { get; set; }
        public abstract string Departure { get; set; }

        public abstract string Destination { get; set; }

        public abstract DateTime Date { get; set; }

        public abstract short? Seats { get; set; }

        public abstract short? BasePrice { get; set; }

        public virtual ICollection<Booking> BookingSet { get; set; }


        //void OpenSales(string departure, string destination, DateTime dateTime, short? basePrice);
    }
}
