using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class FuncionarioConfiguration : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfiguration()
        {
            ToTable("Funcionario");
            HasKey(x => x.Id);

            Property(x => x.Nome).HasMaxLength(150);
            Property(x => x.TelefoneCelular).HasMaxLength(11);
            Property(x => x.TelefoneFixo).HasMaxLength(11);
            Property(x => x.Cpf).HasMaxLength(11);
            Property(x => x.Email).HasMaxLength(150);

            HasRequired(x => x.Empresa).WithMany().HasForeignKey(x => x.EmpresaId).WillCascadeOnDelete(true);
        }
    }
}
