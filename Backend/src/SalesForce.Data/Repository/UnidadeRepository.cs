using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using Microsoft.EntityFrameworkCore;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using SalesForce.Data.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ERP.Data.Repository
{
    public class UnidadeRepository : Repository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(SalesForceDbContext context, ICache cache) : base(context, cache) { }

        public async Task<ResponseModel<Unidade>> RecuperarTodos(PaginationFilter filter)
        {
            var list = await Cache.GetListAsync<Unidade>(MethodBase.GetCurrentMethod().Name);
            if (list.Count() <= 0)
            {
                var data = await Db.Unidades
                    .AsNoTracking()
                    .ToListAsync();
                var count = await Db.Unidades.CountAsync();

                await Cache.SetListAsync<Unidade>(MethodBase.GetCurrentMethod().Name, data);

                return new ResponseModel<Unidade>(data, count);
            }

            return new ResponseModel<Unidade>(list, list.Count());
        }
        public bool JaExiste(Guid id, string sigla)
        {
            return Buscar(f => f.Sigla == sigla && f.Id != id).Result.Any();
        }
        public bool Encontrou(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();
        }
    }
}
