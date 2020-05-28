using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models
{
    public class Passenger
    {
        [Key]
        public int PersonID { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }
    }
}
