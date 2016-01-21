using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Services
{
    public interface ICaixaService : IServiceBase<Caixa>
    {
        IEnumerable<Caixa> ObterTodos(int empresaId);

        IEnumerable<Caixa> ObterTodos(int empresaId, DateTime dataRetirada);
    }
}
