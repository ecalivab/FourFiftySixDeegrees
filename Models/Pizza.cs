using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace FortyFiftySixDeegrees.Models
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string URL { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionFI { get; set; }
    
    }
}