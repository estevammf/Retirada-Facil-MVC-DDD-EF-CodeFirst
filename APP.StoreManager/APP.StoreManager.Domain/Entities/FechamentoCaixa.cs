using System;

namespace APP.StoreManager.Domain.Entities
{
    public class FechamentoCaixa
    {
        public int Id { get; set; }
        public decimal SomaRetirada { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public int CaixaId { get; set; }
        public virtual Caixa Caixa { get; set; }
    }
}
