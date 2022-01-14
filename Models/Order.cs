using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace FortyFiftySixDeegrees.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required(ErrorMessage = "You must provide a Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string TelephoneNumber { get; set; }
        public string Data { get; set; }
        public double TotalAmount { get; set; }
        public bool Ready { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }   
}