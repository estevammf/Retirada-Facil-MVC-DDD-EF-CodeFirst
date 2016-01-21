using System.Collections.Generic;
using System.Linq;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Application
{
    public class RendimentoAppService : AppServiceBase<Rendimento>, IRendimentoAppService
    {
        private readonly IRendimentoService _rendimentoService;
        public RendimentoAppService(IRendimentoService rendimentoService)
            : base(rendimentoService)
        {
            _rendimentoService = rendimentoService;
        }

        public IEnumerable<Rendimento> ObtemRendimentos(int empresaId)
        {
            return _rendimentoService.ObtemRendimentos(empresaId);
        }

        public IEnumerable<Rendimento> ObtemRendimentoAnual(int empresaId, int ano)
        {
            return _rendimentoService.ObtemRendimentoAnual(empresaId, ano);
        }

        public IEnumerable<Rendimento> ObtemRendimentoMensal(int empresaId, int mes, int ano)
        {
            return _rendimentoService.ObtemRendimentoMensal(empresaId, mes, ano);
        }

        public decimal TotalRendimentos(IEnumerable<Rendimento> rendimentos)
        {
            return rendimentos.Aggregate<Rendimento, decimal>(0, (current, rendimento) => current + rendimento.TotalRendimento);
        }
    }
}
