using System;

namespace APP.StoreManager.Domain.Entities
{
    public class EntidadeBase
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
