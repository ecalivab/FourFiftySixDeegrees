namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removesetfromvirtualPizzaOrderdetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas");
            DropIndex("dbo.OrderDetails", new[] { "PizzaID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.OrderDetails", "PizzaID");
            AddForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas", "PizzaId", cascadeDelete: true);
        }
    }
}
