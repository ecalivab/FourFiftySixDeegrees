using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FortyFiftySixDeegrees.Models;

namespace FortyFiftySixDeegrees.Models
{
    public class OrderDetails
    {
        public int OrderDetailsID { get; set; }
        public int PizzaId { get; set; }
        [ForeignKey("PizzaId")]
        public virtual Pizza Pizza { get; set; }
        public int Quantity { get; set; }

        public virtual Order OrderParent { get; set; }
    }
}