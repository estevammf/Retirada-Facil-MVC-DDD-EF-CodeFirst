using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class PermissaoAcessoRepository : RepositoryBase<PermissaoAcesso>, IPermissaoAcessoRepository
    {
        public PermissaoAcesso ObtemPermissaoAcesso(string id)
        {
            return Db.PermissoesAcesso.First(x => x.Id.ToString() == id);
        }
    }
}
