using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class CaixaConfiguration : EntityTypeConfiguration<Caixa>
    {
        public CaixaConfiguration()
        {
            ToTable("Caixa");

            HasKey(x => x.Id);

            Property(x => x.NumeroIdentificador);
            Property(x => x.DataCadastro);

            HasRequired(x => x.Empresa).WithMany().HasForeignKey(x => x.EmpresaId).WillCascadeOnDelete(true);
            HasRequired(x => x.Operador).WithMany().HasForeignKey(x => x.FuncionarioId);

        }
    }
}
