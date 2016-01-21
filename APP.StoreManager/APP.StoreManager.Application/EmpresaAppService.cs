using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class EmpresaAppService : AppServiceBase<Empresa>, IEmpresaAppService
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaAppService(IEmpresaService empresaService) : base(empresaService)
        {
            _empresaService = empresaService;
        }
    }
}
