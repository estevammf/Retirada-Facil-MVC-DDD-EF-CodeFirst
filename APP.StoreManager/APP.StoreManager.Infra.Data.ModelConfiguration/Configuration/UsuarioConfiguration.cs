using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            HasRequired(x => x.Empresa).WithMany(x=>x.Usuarios).HasForeignKey(x => x.EmpresaId).WillCascadeOnDelete(true);

            ToTable("AspNetUsers");
        }
    }
}