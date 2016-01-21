using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Domain.Interfaces.Repositories
{
    public interface IRetiradaRepository : IRepositoryBase<Retirada>
    {
        IEnumerable<Retirada> ObtemRetiradasCaixa(int caixaId);

        IEnumerable<Retirada> ObtemRetiradasPorData(DateTime data, int idEmpresa);
    }
}
