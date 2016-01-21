using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Infra.Data.ModelConfiguration.Configuration;

namespace APP.StoreManager.Infra.Data.Context
{
    public class StoreManagerContext : DbContext
    {
        public StoreManagerContext() : base ("StoreManagerConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PermissaoAcesso> PermissoesAcesso { get; set; }
        public DbSet<Retirada> Retiradas { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<FechamentoCaixa> FechamentoCaixas { get; set; }
        public DbSet<Rendimento> Rendimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new FuncionarioConfiguration());
            modelBuilder.Configurations.Add(new EmpresaConfiguration());
            modelBuilder.Configurations.Add(new FornecedorConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new PermissaoAcessoConfiguration());
            modelBuilder.Configurations.Add(new RetiradaConfiguration());
            modelBuilder.Configurations.Add(new CaixaConfiguration());
            modelBuilder.Configurations.Add(new FechamentoCaixaConfiguration());
            modelBuilder.Configurations.Add(new RendimentoConfiguration());

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                    DateTime localDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                    entry.Property("DataCadastro").CurrentValue = localDatetime;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}
