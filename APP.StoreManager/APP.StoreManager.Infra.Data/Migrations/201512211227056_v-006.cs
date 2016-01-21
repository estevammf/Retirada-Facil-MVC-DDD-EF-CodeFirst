namespace APP.StoreManager.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rendimento", "TotalRendimento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rendimento", "TotalRendimento");
        }
    }
}
