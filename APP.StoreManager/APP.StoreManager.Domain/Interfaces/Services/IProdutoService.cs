using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        IEnumerable<Produto> BuscarPorNome(string nome);
    }
}
