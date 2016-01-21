using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Services
{
    public interface IFuncionarioService : IServiceBase<Funcionario>
    {
        IEnumerable<Funcionario> ObterTodos(int empresaId);
    }
}
