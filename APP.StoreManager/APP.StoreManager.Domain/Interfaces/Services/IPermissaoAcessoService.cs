using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Services
{
    public interface IPermissaoAcessoService : IServiceBase<PermissaoAcesso>
    {
        PermissaoAcesso ObtemPermissaoAcesso(string id);
    }
}
