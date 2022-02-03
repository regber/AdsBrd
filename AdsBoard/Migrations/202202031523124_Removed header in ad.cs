namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedheaderinad : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ads", "Header");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "Header", c => c.String());
        }
    }
}
