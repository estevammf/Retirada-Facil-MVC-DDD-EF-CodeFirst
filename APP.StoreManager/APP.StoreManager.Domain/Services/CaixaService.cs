using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class CaixaService : ServiceBase<Caixa>, ICaixaService
    {
        private readonly ICaixaRepository _caixaRepository;
        public CaixaService(ICaixaRepository caixaRepository)
            : base(caixaRepository)
        {
            _caixaRepository = caixaRepository;
        }

        public IEnumerable<Caixa> ObterTodos(int empresaId)
        {
            return _caixaRepository.ObterTodos(empresaId);
        }

        public IEnumerable<Caixa> ObterTodos(int empresaId, DateTime dataRetirada)
        {
            return _caixaRepository.ObterTodos(empresaId, dataRetirada);
        }
    }
}
