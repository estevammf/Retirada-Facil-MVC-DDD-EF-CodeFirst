namespace APP.StoreManager.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v002 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Empresas",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Nome = c.String(),
            //            NomeFantasia = c.String(),
            //            Cnpj = c.String(),
            //            Telefone = c.String(),
            //            Estado = c.String(),
            //            Cidade = c.String(),
            //            Cep = c.String(),
            //            Numero = c.Int(),
            //            DataCadastro = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.AspNetUsers", "EmpresaId", c => c.Int(nullable: false));
            //CreateIndex("dbo.AspNetUsers", "EmpresaId");
            //AddForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresas");
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaId" });
            DropColumn("dbo.AspNetUsers", "EmpresaId");
            DropTable("dbo.Empresas");
        }
    }
}
