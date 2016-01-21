using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class FornecedorConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfiguration()
        {
            ToTable("Fornecedor");
            HasKey(x => x.Id);

            Property(x => x.Nome).HasMaxLength(150);
            Property(x => x.TelefoneCelular).HasMaxLength(11);
            Property(x => x.TelefoneFixo).HasMaxLength(11);
            Property(x => x.Cnpj);
            Property(x => x.Email).HasMaxLength(150);

            HasRequired(x => x.Empresa).WithMany().HasForeignKey(x => x.EmpresaId).WillCascadeOnDelete(true);
        }
    }
}
