using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace APP.StoreManager.Domain.Entities
{
    public class Caixa : EntidadeBase
    {
        public string NumeroIdentificador { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Operador { get; set; }

        public virtual ICollection<Retirada> Retiradas { get; set; } 
    }
}
