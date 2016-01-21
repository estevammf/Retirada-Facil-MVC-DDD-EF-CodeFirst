namespace APP.StoreManager.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v003 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cliente", newName: "Funcionario");
            CreateTable(
                "dbo.Caixa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroIdentificador = c.String(maxLength: 100, unicode: false),
                        FuncionarioId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId, cascadeDelete: true)
                .ForeignKey("dbo.Funcionario", t => t.FuncionarioId)
                .Index(t => t.FuncionarioId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.FechamentoCaixa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SomaRetirada = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraRegistro = c.DateTime(nullable: false),
                        CaixaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Caixa", t => t.CaixaId)
                .Index(t => t.CaixaId);
            
            CreateTable(
                "dbo.Retirada",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataHoraRegistro = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumeroEnvelope = c.String(maxLength: 100, unicode: false),
                        CaixaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Caixa", t => t.CaixaId)
                .Index(t => t.CaixaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Retirada", "CaixaId", "dbo.Caixa");
            DropForeignKey("dbo.FechamentoCaixa", "CaixaId", "dbo.Caixa");
            DropForeignKey("dbo.Caixa", "FuncionarioId", "dbo.Funcionario");
            DropForeignKey("dbo.Caixa", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.Retirada", new[] { "CaixaId" });
            DropIndex("dbo.FechamentoCaixa", new[] { "CaixaId" });
            DropIndex("dbo.Caixa", new[] { "EmpresaId" });
            DropIndex("dbo.Caixa", new[] { "FuncionarioId" });
            DropTable("dbo.Retirada");
            DropTable("dbo.FechamentoCaixa");
            DropTable("dbo.Caixa");
            RenameTable(name: "dbo.Funcionario", newName: "Cliente");
        }
    }
}
