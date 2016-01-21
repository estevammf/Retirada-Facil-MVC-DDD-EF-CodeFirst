using System;

namespace APP.StoreManager.Domain.Entities
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public int Quantidade { get; set; }
        public int FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }

    }
}
