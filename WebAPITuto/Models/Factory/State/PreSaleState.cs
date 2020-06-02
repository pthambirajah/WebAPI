using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    class PreSaleState : State
    {
        public override string BuyTickets()
        {
            return "Cannot buy ticket yet";

        }
    }
}
