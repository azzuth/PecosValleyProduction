namespace Pvp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faqsAndPublications2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faqs", "Question", c => c.String());
            AddColumn("dbo.Faqs", "Answer", c => c.String());
            AddColumn("dbo.Faqs", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Faqs", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faqs", "DisplayOrder");
            DropColumn("dbo.Faqs", "Active");
            DropColumn("dbo.Faqs", "Answer");
            DropColumn("dbo.Faqs", "Question");
        }
    }
}
