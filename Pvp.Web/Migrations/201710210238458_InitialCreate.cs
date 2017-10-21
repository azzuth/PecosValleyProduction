namespace Pvp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestedDateTime = c.DateTime(nullable: false),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        UserId = c.Int(),
                        Responded = c.Boolean(),
                        RespondedOn = c.DateTime(),
                        LocationId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ViewedDate = c.DateTime(nullable: false),
                        ViewedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        PhoneNumber = c.String(),
                        HoursOfOperation = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StateAbbreviation = c.String(),
                        ZipCode = c.String(),
                        Comments = c.String(),
                        MenuId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BioTrackThcMenus", t => t.MenuId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.BioTrackThcMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Name = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        UserId = c.Int(),
                        Responded = c.Boolean(),
                        RespondedOn = c.DateTime(),
                        LocationId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ViewedDate = c.DateTime(nullable: false),
                        ViewedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.CustomerReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Approved = c.Boolean(),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        IpAddress = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ViewedDate = c.DateTime(nullable: false),
                        ViewedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.IpBlacklists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAddress = c.String(),
                        BannedReason = c.String(),
                        DateBanned = c.DateTime(nullable: false),
                        BanExpires = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestAttemptLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAddress = c.String(),
                        Date = c.DateTime(nullable: false),
                        AttemptType = c.String(),
                        Rejected = c.Boolean(nullable: false),
                        SystemComment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                        Rating = c.Int(),
                        DateOfReview = c.DateTime(nullable: false),
                        PriorityOfDisplay = c.Int(nullable: false),
                        SourceOfReview = c.String(),
                        UrlToReview = c.String(),
                        DiscountRedeemed = c.Boolean(nullable: false),
                        DiscountRedeemedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerReviews", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.ContactRequests", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.AppointmentRequests", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "MenuId", "dbo.BioTrackThcMenus");
            DropIndex("dbo.CustomerReviews", new[] { "LocationId" });
            DropIndex("dbo.ContactRequests", new[] { "LocationId" });
            DropIndex("dbo.Locations", new[] { "MenuId" });
            DropIndex("dbo.AppointmentRequests", new[] { "LocationId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.RequestAttemptLogs");
            DropTable("dbo.IpBlacklists");
            DropTable("dbo.CustomerReviews");
            DropTable("dbo.ContactRequests");
            DropTable("dbo.BioTrackThcMenus");
            DropTable("dbo.Locations");
            DropTable("dbo.AppointmentRequests");
        }
    }
}
