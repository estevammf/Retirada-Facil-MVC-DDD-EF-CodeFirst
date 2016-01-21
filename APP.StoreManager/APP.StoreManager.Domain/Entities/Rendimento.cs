using System;
using APP.StoreManager.Domain.Enum;

namespace APP.StoreManager.Domain.Entities
{
    public class Rendimento : EntidadeBase
    {
        public TipoTransacaoBanesfacilEnum TipoTransacao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataReferencia { get; set; }
        public decimal TotalRendimento { get; set; }
    }
}
