using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        IEnumerable<Produto> BuscarPorNome(string nome);
    }
}
