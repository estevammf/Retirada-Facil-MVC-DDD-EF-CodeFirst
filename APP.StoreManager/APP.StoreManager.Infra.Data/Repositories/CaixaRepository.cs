using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Domain.Interfaces.Repositories;

namespace APP.StoreManager.Infra.Data.Repositories
{
    public class CaixaRepository : RepositoryBase<Caixa>, ICaixaRepository
    {
        public IEnumerable<Caixa> ObterTodos(int empresaId)
        {
            var caixas = Db.Caixas
                .Where(x => x.EmpresaId == empresaId)
                .Include(c => c.Empresa)
                .Include(c => c.Operador)
                .Include(c=>c.Retiradas);

            return caixas;
        }

        public IEnumerable<Caixa> ObterTodos(int empresaId, DateTime dataRetirada)
        {
            var retiradas = Db.Retiradas.
                AsQueryable()
                .Where(x => x.DataHoraRegistro.Day == dataRetirada.Day &&
                            x.DataHoraRegistro.Month == dataRetirada.Month &&
                            x.DataHoraRegistro.Year == dataRetirada.Year).ToList();


            var caixas = Db.Caixas
                .AsQueryable()
                .Include(x => x.Empresa)
                .Include(x => x.Operador)
                .Where(x=>x.EmpresaId == empresaId).ToList();

            foreach (var retirada in retiradas)
            {
                foreach (var caixa in caixas)
                {
                    if(caixa.Id == retirada.CaixaId)
                        caixa.Retiradas.Add(retirada);
                }
            }

            return caixas;
        }
    }
}
