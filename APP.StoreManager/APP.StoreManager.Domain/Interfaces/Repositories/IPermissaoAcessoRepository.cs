using System.Security.Cryptography.X509Certificates;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Repositories
{
    public interface IPermissaoAcessoRepository : IRepositoryBase<PermissaoAcesso>
    {
        PermissaoAcesso ObtemPermissaoAcesso(string id);
    }
}
