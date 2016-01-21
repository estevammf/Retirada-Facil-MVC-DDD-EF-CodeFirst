using System;
using System.Collections.Generic;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class CaixaAppService : AppServiceBase<Caixa>, ICaixaAppService
    {
        private readonly ICaixaService _caixaService;
        public CaixaAppService(ICaixaService caixaService)
            : base(caixaService)
        {
            _caixaService = caixaService;
        }

        public IEnumerable<Caixa> ObterTodos(int empresaId)
        {
            return _caixaService.ObterTodos(empresaId);
        }

        public IEnumerable<Caixa> ObterTodos(int empresaId, DateTime dataRetirada)
        {
            return _caixaService.ObterTodos(empresaId, dataRetirada);
        }

        public DateTime ObtemDataOperacao(DateTime data)
        {

            if (data.DayOfWeek == DayOfWeek.Saturday)
            {
                data = DateTime.Now.AddDays(2);
            }

            else if (data.DayOfWeek == DayOfWeek.Sunday)
            {
                data = DateTime.Now.AddDays(1);
            }

            return data;
        }
    }
}
