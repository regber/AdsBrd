namespace AdsBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinheritanceDBclasstoViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Discriminator");
        }
    }
}
