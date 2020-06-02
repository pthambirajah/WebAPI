using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPITuto.Models.Factory;

namespace WebAPITuto.Models
{
    [NotMapped]
    class Jet : Flight
    {
        public Jet( string departure, string destination, DateTime date, short? basePrice, short? seats)
        {
    
            Departure = departure;
            Destination = destination;
            Date = date;
            Seats = seats;
            BasePrice = basePrice;
        }
        [Key]
        public override int FlightNo { get; set; }

        [StringLength(50), MinLength(3)]
        public override string Departure { get; set; }

        [StringLength(50), MinLength(3)]
        public override string Destination { get; set; }

        public override DateTime Date { get; set; }

        [Required]
        public override short? Seats { get; set; }

        [Required]
        public override short? BasePrice { get; set; }
        
    }
}
