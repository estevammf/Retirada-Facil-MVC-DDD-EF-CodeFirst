using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Services
{
    public interface IRendimentoService : IServiceBase<Rendimento>
    {
        IEnumerable<Rendimento> ObtemRendimentos(int empresaId);

        IEnumerable<Rendimento> ObtemRendimentoAnual(int empresaId, int ano);

        IEnumerable<Rendimento> ObtemRendimentoMensal(int empresaId, int mes, int ano);
    }
}
