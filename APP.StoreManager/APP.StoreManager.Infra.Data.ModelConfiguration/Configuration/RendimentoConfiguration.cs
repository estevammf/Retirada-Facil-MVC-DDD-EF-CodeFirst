using APP.StoreManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class RendimentoConfiguration : EntityTypeConfiguration<Rendimento>
    {
        public RendimentoConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.DataCadastro);
            Property(x => x.Quantidade);
            Property(x => x.TipoTransacao);
            Property(x => x.DataReferencia);

            HasRequired(x => x.Empresa).WithMany().HasForeignKey(x => x.EmpresaId).WillCascadeOnDelete(true);
        }
    }
}
