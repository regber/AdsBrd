namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeadtoadforcars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Producer", c => c.String());
            AddColumn("dbo.Ads", "Model", c => c.String());
            AddColumn("dbo.Ads", "EngineVolume", c => c.Double(nullable: false));
            AddColumn("dbo.Ads", "RealeaseYear", c => c.Int(nullable: false));
            AddColumn("dbo.Ads", "Drive", c => c.String());
            AddColumn("dbo.Ads", "Transmission", c => c.String());
            AddColumn("dbo.Ads", "FuelType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "FuelType");
            DropColumn("dbo.Ads", "Transmission");
            DropColumn("dbo.Ads", "Drive");
            DropColumn("dbo.Ads", "RealeaseYear");
            DropColumn("dbo.Ads", "EngineVolume");
            DropColumn("dbo.Ads", "Model");
            DropColumn("dbo.Ads", "Producer");
        }
    }
}
