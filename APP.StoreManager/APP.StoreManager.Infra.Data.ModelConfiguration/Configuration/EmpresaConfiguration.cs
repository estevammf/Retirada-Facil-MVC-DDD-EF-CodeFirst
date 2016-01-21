using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            ToTable("Empresa");
            HasKey(x => x.Id);

            Property(x => x.Cnpj).HasMaxLength(20);
            Property(x => x.Cidade);
            Property(x => x.Estado);
            Property(x => x.Cep);
            Property(x => x.Numero);

            Property(x => x.Nome).HasMaxLength(150);
            Property(x => x.NomeFantasia).HasMaxLength(150);
            Property(x => x.Telefone).HasMaxLength(11);
            Property(x => x.DataCadastro);

            //HasMany(x=>x.Clientes).WithRequired(x=>x.Empresa).WillCascadeOnDelete(true);
            //HasMany(x => x.Fornecedores).WithRequired(x => x.Empresa).WillCascadeOnDelete(true);
            //HasMany(x => x.Clientes).WithRequired(x => x.Empresa).WillCascadeOnDelete(true);
            //HasMany(x => x.Produtos).WithRequired(x => x.Empresa).WillCascadeOnDelete(true);
        }
    }
}
