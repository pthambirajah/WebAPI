using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public abstract class Flight
    {
       
        [Key]
        public abstract int FlightNo { get; set; }
        public abstract string Departure { get; set; }

      
        public abstract string Destination { get; set; }

        public abstract DateTime Date { get; set; }

      
        public abstract short? Seats { get; }

        //A supprimer et recréer la BD
        public abstract short? AvailableSeats { get; set; }

        public abstract short? BasePrice { get; set; }

        public virtual ICollection<Booking> BookingSet { get; set; }


    }
}
