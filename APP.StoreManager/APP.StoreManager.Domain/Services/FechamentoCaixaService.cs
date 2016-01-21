using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;

namespace APP.StoreManager.Domain.Services
{
    public class FechamentoCaixaService : ServiceBase<FechamentoCaixa>, IFechamentoCaixaService
    {
        private readonly IFechamentoCaixaRepository _fechamentoCaixaRepository;
        public FechamentoCaixaService(IFechamentoCaixaRepository fechamentoCaixaRepository)
            : base(fechamentoCaixaRepository)
        {
            _fechamentoCaixaRepository = fechamentoCaixaRepository;
        }
    }
}
