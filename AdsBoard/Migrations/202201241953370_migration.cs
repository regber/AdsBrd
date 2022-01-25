namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Text = c.String(),
                        MainImage_Id = c.Int(),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.MainImage_Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.MainImage_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Ad_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ads", t => t.Ad_Id, cascadeDelete: true)
                .Index(t => t.Ad_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        EMail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Ads", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Ads", "MainImage_Id", "dbo.Images");
            DropForeignKey("dbo.Images", "Ad_Id", "dbo.Ads");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Images", new[] { "Ad_Id" });
            DropIndex("dbo.Ads", new[] { "Account_Id" });
            DropIndex("dbo.Ads", new[] { "MainImage_Id" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Images");
            DropTable("dbo.Ads");
            DropTable("dbo.Accounts");
        }
    }
}
