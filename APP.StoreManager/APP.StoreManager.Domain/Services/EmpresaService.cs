using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class EmpresaService : ServiceBase<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaService(IEmpresaRepository empresaRepository)
            : base(empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
    }
}
