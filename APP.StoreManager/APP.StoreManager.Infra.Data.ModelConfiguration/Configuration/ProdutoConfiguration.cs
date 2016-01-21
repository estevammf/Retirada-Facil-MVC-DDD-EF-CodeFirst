using System.Data.Entity.ModelConfiguration;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Infra.Data.ModelConfiguration.Configuration
{
    public class ProdutoConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            ToTable("Produto");
            HasKey(x => x.Id);

            Property(x => x.Nome);
            Property(x => x.Descrição).HasMaxLength(500);
            Property(x => x.PrecoCompra);
            Property(x => x.PrecoVenda);
            Property(x => x.Quantidade);

            HasRequired(x => x.Fornecedor).WithMany(x=>x.Produtos).HasForeignKey(x => x.FornecedorId).WillCascadeOnDelete(true);
        }
    }
}
