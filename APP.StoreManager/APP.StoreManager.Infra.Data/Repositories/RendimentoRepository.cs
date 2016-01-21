using System.Collections.Generic;
using System.Linq;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class RendimentoRepository : RepositoryBase<Rendimento>, IRendimentoRepository
    {

        public IEnumerable<Rendimento> ObtemRendimentos(int empresaId)
        {
            var rendimentos = Db.Rendimentos.Where(x => x.EmpresaId == empresaId);
            return rendimentos;
        }

        public IEnumerable<Rendimento> ObtemRendimentoAnual(int empresaId, int ano)
        {
            var rendimentos = Db.Rendimentos.Where(x => x.EmpresaId == empresaId && x.DataReferencia.Year == ano);
            return rendimentos;
        }

        public IEnumerable<Rendimento> ObtemRendimentoMensal(int empresaId, int mes, int ano)
        {
            var rendimentos = Db.Rendimentos.Where(x => x.EmpresaId == empresaId && x.DataReferencia.Month == mes && x.DataReferencia.Year == ano);
            return rendimentos;
        }
    }
}
