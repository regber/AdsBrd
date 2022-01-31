namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepricetype2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
