using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.StoreManager.Domain.Entities
{
    public class Retirada
    {
        public int Id { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public decimal Valor { get; set; }
        public string NumeroEnvelope { get; set; }

        public int CaixaId { get; set; }
        public virtual Caixa Caixa { get; set; }
    }
}
