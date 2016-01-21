using System;
using System.Collections.Generic;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class RetiradaAppService : AppServiceBase<Retirada>, IRetiradaAppService
    {
        private readonly IRetiradaService _retiradaService;
        public RetiradaAppService(IRetiradaService retiradaService)
            : base(retiradaService)
        {
            _retiradaService = retiradaService;
        }

        public IEnumerable<Retirada> ObtemRetiradasCaixa(int caixaId)
        {
            return _retiradaService.ObtemRetiradasCaixa(caixaId);
        }

        public IEnumerable<Retirada> ObtemRetiradasPorData(DateTime data, int idEmpresa)
        {
            return _retiradaService.ObtemRetiradasPorData(data, idEmpresa);

        }
    }
}
