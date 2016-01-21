using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class RetiradaRepository : RepositoryBase<Retirada>, IRetiradaRepository
    {
        public IEnumerable<Retirada> ObtemRetiradasCaixa(int caixaId)
        {
            return Db.Retiradas.Where(x => x.CaixaId == caixaId).Include(x=>x.Caixa.Operador);
        }

        public IEnumerable<Retirada> ObtemRetiradasPorData(DateTime data, int idEmpresa)
        {
            var caixas = Db.Caixas.Where(x => x.EmpresaId == idEmpresa).Select(x=>x.Id).ToList();

            var caixasRet = Db.Retiradas
               .Where(x => caixas.Contains(x.Caixa.Id) &&
                           x.DataHoraRegistro.Day == data.Day &&
                           x.DataHoraRegistro.Month == data.Month &&
                           x.DataHoraRegistro.Year == data.Year)
                .Include(x => x.Caixa.Operador)
               .ToList();

            return caixasRet;


            //return Db.Retiradas.Where(x => DbFunctions.TruncateTime(x.DataHoraRegistro.Date) == DbFunctions.TruncateTime(data.Date));
            //return Db.Retiradas.Where(x => 
            //    x.DataHoraRegistro.Day == data.Day &&
            //    x.DataHoraRegistro.Month == data.Month &&
            //    x.DataHoraRegistro.Year == data.Year)
        }
    }
}
