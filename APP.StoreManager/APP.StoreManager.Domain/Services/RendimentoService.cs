using APP.StoreManager.Domain.Entities;
using System.Collections.Generic;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class RendimentoService : ServiceBase<Rendimento>, IRendimentoService
    {
        private readonly IRendimentoRepository _rendimentoRepository;
        public RendimentoService(IRendimentoRepository rendimentoRepository)
            : base(rendimentoRepository)
        {
            _rendimentoRepository = rendimentoRepository;
        }

        public IEnumerable<Rendimento> ObtemRendimentos(int empresaId)
        {
            return _rendimentoRepository.ObtemRendimentos(empresaId);
        }

        public IEnumerable<Rendimento> ObtemRendimentoAnual(int empresaId, int ano)
        {
            return _rendimentoRepository.ObtemRendimentoAnual(empresaId, ano);
        }

        public IEnumerable<Rendimento> ObtemRendimentoMensal(int empresaId, int mes, int ano)
        {
            return _rendimentoRepository.ObtemRendimentoMensal(empresaId, mes, ano);
        }
    }
}
