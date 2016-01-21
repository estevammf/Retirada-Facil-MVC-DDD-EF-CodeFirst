using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.StoreManager.Domain.Enum;

namespace APP.Store.Mvc.Models
{
    public class RendimentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Tipo Transação é obrigatório")]
        public TipoTransacaoBanesfacilEnum TipoTransacao { get; set; }

        [Required(ErrorMessage = "Campo Quantidade é obrigatório")]
        public int Quantidade { get; set; }
        public DateTime DataReferencia { get; set; }

        [Required(ErrorMessage = "Campo Data de Referência é obrigatório")]
        public string DataReferenciaString { get; set; }
        public decimal TotalRendimento { get; set; }
        public int EmpresaId { get; set; }

      
    }
}
