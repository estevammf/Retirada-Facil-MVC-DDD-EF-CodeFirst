namespace APP.StoreManager.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rendimento", "DataReferencia", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rendimento", "DataReferencia");
        }
    }
}
