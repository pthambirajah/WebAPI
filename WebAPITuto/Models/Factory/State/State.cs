using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITuto.Models.Factory
{
    //Here we are declaring all the methods (actually one) the differents states should implement.
    abstract class State
    {
        protected Flight _context;

        //This is a backreference to the context (boeing).
        //Could be used to transite from a state to another.
        public void SetContext(Flight context)
        {
            this._context = context;
        }

        public abstract string BuyTickets();
    }
}
