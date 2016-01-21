using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class RetiradaService : ServiceBase<Retirada>, IRetiradaService
    {
        private readonly IRetiradaRepository _retiradaRepository;
        public RetiradaService(IRetiradaRepository retiradaRepository)
            : base(retiradaRepository)
        {
            _retiradaRepository = retiradaRepository;
        }

        public IEnumerable<Retirada> ObtemRetiradasCaixa(int caixaId)
        {
            return _retiradaRepository.ObtemRetiradasCaixa(caixaId);
        }

        public IEnumerable<Retirada> ObtemRetiradasPorData(DateTime data, int idEmpresa)
        {
            return _retiradaRepository.ObtemRetiradasPorData(data, idEmpresa);
        }
    }
}
