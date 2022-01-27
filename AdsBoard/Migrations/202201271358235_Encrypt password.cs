namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Encryptpassword : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        EncryptPassword = c.String(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Text = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Account_Id = c.Int(nullable: false),
                        MainImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.MainImage_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.MainImage_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                        Birthday = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        EMail = c.String(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Ads", "MainImage_Id", "dbo.Images");
            DropForeignKey("dbo.Images", "Ad_Id", "dbo.Ads");
            DropForeignKey("dbo.Ads", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Images", new[] { "Ad_Id" });
            DropIndex("dbo.Ads", new[] { "MainImage_Id" });
            DropIndex("dbo.Ads", new[] { "Account_Id" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Images");
            DropTable("dbo.Ads");
            DropTable("dbo.Accounts");
        }
    }
}
