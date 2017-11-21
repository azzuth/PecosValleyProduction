namespace Pvp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faqsAndPublications3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "PageLocation", c => c.Int(nullable: false));
            AddColumn("dbo.Publications", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Active");
            DropColumn("dbo.Publications", "PageLocation");
        }
    }
}
