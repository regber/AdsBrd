namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepricetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ads", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "Price", c => c.Double(nullable: false));
        }
    }
}
