namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "Discriminator");
            DropColumn("dbo.Ads", "Discriminator");
            DropColumn("dbo.UserProfiles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Ads", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Accounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
