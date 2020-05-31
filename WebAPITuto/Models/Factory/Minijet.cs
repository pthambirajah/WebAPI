using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    [NotMapped]
    class Minijet : Flight
    {
        public Minijet( string departure, string destination, DateTime date, short? basePrice)
        {
           
            Departure = departure;
            Destination = destination;
            Date = date;
            Seats = 15;
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
        public override short? Seats { get; }

        //A supprimer et recréer la BD
        public override short? AvailableSeats { get; set; }

        [Required]
        public override short? BasePrice { get; set; }

        //On a enlevé virtual, jsp ce que ça va faire. (Au lieu de override)
        /*public override ICollection<Booking> BookingSet { get; set; }*/
    }
}
