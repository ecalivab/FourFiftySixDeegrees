namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedefineOrdernomoreneedofOrderDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "PizzaID" });
            RenameColumn(table: "dbo.OrderDetails", name: "OrderID", newName: "Order_OrderID");
            AddColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Order_OrderID", c => c.Int());
            AlterColumn("dbo.OrderDetails", "Amount", c => c.Double(nullable: false));
            CreateIndex("dbo.OrderDetails", "Order_OrderID");
            AddForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderID" });
            AlterColumn("dbo.OrderDetails", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Order_OrderID", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Quantity");
            RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderID", newName: "OrderID");
            CreateIndex("dbo.OrderDetails", "PizzaID");
            CreateIndex("dbo.OrderDetails", "OrderID");
            AddForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "PizzaID", "dbo.Pizzas", "PizzaId", cascadeDelete: true);
        }
    }
}
