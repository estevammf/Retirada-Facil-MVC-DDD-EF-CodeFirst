using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using System.Collections.Generic;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
            : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario ObterPorId(string id)
        {
           return _usuarioService.ObterPorId(id);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _usuarioService.ObterTodos();
        }

        public void DesativarLock(string id)
        {
            _usuarioService.DesativarLock(id);
        }
    }
}
