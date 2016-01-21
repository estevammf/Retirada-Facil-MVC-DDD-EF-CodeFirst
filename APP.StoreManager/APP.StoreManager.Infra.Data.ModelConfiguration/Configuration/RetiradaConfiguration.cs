using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class RetiradaConfiguration : EntityTypeConfiguration<Retirada>
    {
        public RetiradaConfiguration()
        {
            ToTable("Retirada");

            HasKey(x => x.Id);

            Property(x => x.Valor);
            Property(x => x.NumeroEnvelope);
            Property(x => x.DataHoraRegistro);

            HasRequired(x => x.Caixa).WithMany(x=>x.Retiradas).HasForeignKey(x => x.CaixaId);
        }
    }
}
