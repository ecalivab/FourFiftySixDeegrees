using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortyFiftySixDeegrees.Models
{
    public class PartialOrder
    {
        public int PizzaID { get; set; }
        public string PizzaName { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }

}