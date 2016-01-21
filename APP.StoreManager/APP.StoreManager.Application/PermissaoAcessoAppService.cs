using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class PermissaoAcessoAppService : AppServiceBase<PermissaoAcesso>, IPermissaoAcessoAppService
    {
        private readonly IPermissaoAcessoService _permissaoAcessoService;
        public PermissaoAcessoAppService(IPermissaoAcessoService permissaoAcessoService)
            : base(permissaoAcessoService)
        {
            _permissaoAcessoService = permissaoAcessoService;
        }

        public PermissaoAcesso ObtemPermissaoAcesso(string id)
        {
            return _permissaoAcessoService.ObtemPermissaoAcesso(id);
        }
    }
}
