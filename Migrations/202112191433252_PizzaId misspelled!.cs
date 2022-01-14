namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaIdmisspelled : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OrderDetails", new[] { "PizzaID" });
            CreateIndex("dbo.OrderDetails", "PizzaId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderDetails", new[] { "PizzaId" });
            CreateIndex("dbo.OrderDetails", "PizzaID");
        }
    }
}
