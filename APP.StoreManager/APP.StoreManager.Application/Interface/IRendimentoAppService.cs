using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Application.Interface
{
    public interface IRendimentoAppService : IAppServiceBase<Rendimento>
    {
        IEnumerable<Rendimento> ObtemRendimentos(int empresaId);

        IEnumerable<Rendimento> ObtemRendimentoAnual(int empresaId, int ano);

        IEnumerable<Rendimento> ObtemRendimentoMensal(int empresaId, int mes, int ano);

        decimal TotalRendimentos(IEnumerable<Rendimento> rendimentos);

 }
}
