using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Application.Interface
{
    public interface IFuncionarioAppService : IAppServiceBase<Funcionario>
    {
        IEnumerable<Funcionario> ObterTodos(int empresaId);
    }
}
