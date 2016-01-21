namespace APP.StoreManager.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v004 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rendimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoTransacao = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rendimento", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Rendimento", new[] { "EmpresaId" });
            DropTable("dbo.Rendimento");
        }
    }
}
