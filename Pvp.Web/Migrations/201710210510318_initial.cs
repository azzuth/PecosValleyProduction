namespace Pvp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "LocationId");
            AddForeignKey("dbo.Reviews", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "LocationId", "dbo.Locations");
            DropIndex("dbo.Reviews", new[] { "LocationId" });
            DropColumn("dbo.Reviews", "LocationId");
        }
    }
}
