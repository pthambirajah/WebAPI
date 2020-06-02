using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class SaleState : State
    {
        public override string BuyTickets()
        {
            return "You can buy tickets here : api/flights";

        }
    }
}
