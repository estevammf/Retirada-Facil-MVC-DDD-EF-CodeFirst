using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Application.Interface
{
    public interface ICaixaAppService : IAppServiceBase<Caixa>
    {
        IEnumerable<Caixa> ObterTodos(int empresaId);

        IEnumerable<Caixa> ObterTodos(int empresaId, DateTime dataRetirada);

        DateTime ObtemDataOperacao(DateTime data);
    }
}
