using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;
namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class FechamentoCaixaConfiguration : EntityTypeConfiguration<FechamentoCaixa>
    {
        public FechamentoCaixaConfiguration()
        {
            ToTable("FechamentoCaixa");

            HasKey(x => x.Id);

            Property(x => x.SomaRetirada);
            Property(x => x.DataHoraRegistro);

            HasRequired(x => x.Caixa).WithMany().HasForeignKey(x => x.CaixaId);
        }
    }
}
