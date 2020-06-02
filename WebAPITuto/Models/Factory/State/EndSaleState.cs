using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class EndSaleState : State
    {
        public override string BuyTickets()
        {
            return "All tickets have been saled. Sorry";
        }
    }
}
