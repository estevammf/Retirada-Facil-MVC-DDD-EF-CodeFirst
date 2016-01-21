using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Application.Interface
{
    public interface IPermissaoAcessoAppService : IAppServiceBase<PermissaoAcesso>
    {
        PermissaoAcesso ObtemPermissaoAcesso(string id);
    }
}
