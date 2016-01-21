namespace APP.StoreManager.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 150, unicode: false),
                        Cpf = c.String(maxLength: 11, unicode: false),
                        TelefoneFixo = c.String(maxLength: 11, unicode: false),
                        TelefoneCelular = c.String(maxLength: 11, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 150, unicode: false),
                        NomeFantasia = c.String(maxLength: 150, unicode: false),
                        Cnpj = c.String(maxLength: 20, unicode: false),
                        Telefone = c.String(maxLength: 11, unicode: false),
                        Estado = c.String(maxLength: 100, unicode: false),
                        Cidade = c.String(maxLength: 100, unicode: false),
                        Cep = c.String(maxLength: 100, unicode: false),
                        Numero = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fornecedor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 150, unicode: false),
                        Cnpj = c.String(maxLength: 100, unicode: false),
                        TelefoneFixo = c.String(maxLength: 11, unicode: false),
                        TelefoneCelular = c.String(maxLength: 11, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Descrição = c.String(maxLength: 500, unicode: false),
                        PrecoCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        FornecedorId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId)
                .ForeignKey("dbo.Fornecedor", t => t.FornecedorId, cascadeDelete: true)
                .Index(t => t.FornecedorId)
                .Index(t => t.EmpresaId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.AspNetUsers", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Produto", "FornecedorId", "dbo.Fornecedor");
            DropForeignKey("dbo.Produto", "EmpresaId", "dbo.Empresa");
            DropForeignKey("dbo.Fornecedor", "EmpresaId", "dbo.Empresa");
            DropIndex("dbo.AspNetUsers", new[] { "EmpresaId" });
            DropIndex("dbo.Produto", new[] { "EmpresaId" });
            DropIndex("dbo.Produto", new[] { "FornecedorId" });
            DropIndex("dbo.Fornecedor", new[] { "EmpresaId" });
            DropIndex("dbo.Cliente", new[] { "EmpresaId" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Produto");
            DropTable("dbo.Fornecedor");
            DropTable("dbo.Empresa");
            DropTable("dbo.Cliente");
        }
    }
}
