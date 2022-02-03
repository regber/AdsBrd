namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedfieldname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "ReleaseYear", c => c.Int(nullable: false));
            DropColumn("dbo.Ads", "RealeaseYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "RealeaseYear", c => c.Int(nullable: false));
            DropColumn("dbo.Ads", "ReleaseYear");
        }
    }
}
