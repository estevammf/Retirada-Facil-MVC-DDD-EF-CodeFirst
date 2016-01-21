using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class PermissaoAcessoService : ServiceBase<PermissaoAcesso>, IPermissaoAcessoService
    {
        private readonly IPermissaoAcessoRepository _permissaoAcessoRepository;
        public PermissaoAcessoService(IPermissaoAcessoRepository permissaoAcessoRepository)
            : base(permissaoAcessoRepository)
        {
            _permissaoAcessoRepository = permissaoAcessoRepository;
        }

        public PermissaoAcesso ObtemPermissaoAcesso(string id)
        {
            return _permissaoAcessoRepository.ObtemPermissaoAcesso(id);
        }
    }
}
