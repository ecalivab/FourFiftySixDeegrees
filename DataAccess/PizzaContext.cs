using FortyFiftySixDeegrees.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FortyFiftySixDeegrees.DataAccess
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetails> OrderDetails {set; get;}
    }

}