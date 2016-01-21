using System;
using System.Collections.Generic;

namespace APP.StoreManager.Domain.Entities
{
    public class Fornecedor : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
