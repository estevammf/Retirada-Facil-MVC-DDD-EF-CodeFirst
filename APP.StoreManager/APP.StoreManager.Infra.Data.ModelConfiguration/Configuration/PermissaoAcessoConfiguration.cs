using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class PermissaoAcessoConfiguration : EntityTypeConfiguration<PermissaoAcesso>
    {
        public PermissaoAcessoConfiguration()
        {
            ToTable("AspNetClaims");
            HasKey(x => x.Id);
            Property(x => x.Name);
        }
    }
}
