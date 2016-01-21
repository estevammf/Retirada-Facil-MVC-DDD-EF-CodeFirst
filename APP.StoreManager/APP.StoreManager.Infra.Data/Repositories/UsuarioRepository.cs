using System.Collections.Generic;
using System.Linq;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario ObterPorId(string id)
        {
            return Db.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> ObterTodos(int empresaId)
        {
            return Db.Usuarios.Include("Empresa").Where(x => x.EmpresaId == empresaId);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
           return Db.Usuarios.Include("Empresa");
        }

        public void DesativarLock(string id)
        {
            Db.Usuarios.Find(id).LockoutEnabled = false;
            Db.SaveChanges();
        }
    }
}
