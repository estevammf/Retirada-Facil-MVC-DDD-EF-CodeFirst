using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public IEnumerable<Funcionario> ObterTodos(int empresaId)
        {
            return Db.Funcionarios.Where(x => x.EmpresaId == empresaId).Include(x=>x.Empresa);
        }
    }
}
