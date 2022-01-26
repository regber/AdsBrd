namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinheritanceDBclasstoViewModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Images", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserProfiles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Discriminator");
            DropColumn("dbo.Images", "Discriminator");
            DropColumn("dbo.Ads", "Discriminator");
        }
    }
}
