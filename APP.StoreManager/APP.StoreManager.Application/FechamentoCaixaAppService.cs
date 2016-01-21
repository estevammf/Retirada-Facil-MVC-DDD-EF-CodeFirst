using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class FechamentoCaixaAppService : AppServiceBase<FechamentoCaixa>, IFechamentoCaixaAppService
    {
        private readonly IFechamentoCaixaService _fechamentoCaixaService;
        public FechamentoCaixaAppService(IFechamentoCaixaService fechamentoCaixaService)
            : base(fechamentoCaixaService)
        {
            _fechamentoCaixaService = fechamentoCaixaService;
        }
    }
}
