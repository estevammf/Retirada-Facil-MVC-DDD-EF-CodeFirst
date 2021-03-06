﻿using System;
using System.Collections.Generic;
using APP.StoreManager.Domain.Entities;

namespace APP.StoreManager.Application.Interface
{
    public interface IRetiradaAppService : IAppServiceBase<Retirada>
    {
        IEnumerable<Retirada> ObtemRetiradasCaixa(int caixaId);

        IEnumerable<Retirada> ObtemRetiradasPorData(DateTime data, int idEmpresa);
    }
}
