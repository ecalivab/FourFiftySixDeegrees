namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyinOrderDetails : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OrderDetails", "PizzaID");
            AddForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas", "PizzaId", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Amount", c => c.Double(nullable: false));
            DropForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas");
            DropIndex("dbo.OrderDetails", new[] { "PizzaID" });
        }
    }
}
