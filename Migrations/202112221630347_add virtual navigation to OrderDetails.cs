namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvirtualnavigationtoOrderDetails : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderID", newName: "OrderParent_OrderID");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_Order_OrderID", newName: "IX_OrderParent_OrderID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrderDetails", name: "IX_OrderParent_OrderID", newName: "IX_Order_OrderID");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderParent_OrderID", newName: "Order_OrderID");
        }
    }
}
