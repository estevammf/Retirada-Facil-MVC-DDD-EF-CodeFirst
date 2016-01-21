using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterPorId(string id);
        IEnumerable<Usuario> ObterTodos();
        IEnumerable<Usuario> ObterTodos(int empresaId);
        void DesativarLock(string id);
    }
}
