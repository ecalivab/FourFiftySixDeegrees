namespace FortyFiftySixDeegrees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReadypropertytoOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Ready", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Ready");
        }
    }
}
