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
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(SalesForceDbContext context, ICache cache) : base(context, cache) { }
        public async Task<int> RecuperarQuantidade()
        {
            return await Db.Clientes.AsNoTracking().CountAsync();
        }
        public async Task<ResponseModel<Cliente>> RecuperarTodos(PaginationFilter filter)
        {
            var list = await Cache.GetListAsync<Cliente>(MethodBase.GetCurrentMethod().Name);
            if (list.Count() <= 0)
            {
                var data = await Db.Clientes
                    .Include(x => x.Cidade)              
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)                                      
                    .ToListAsync();
                var count = await Db.Clientes.CountAsync();

                await Cache.SetListAsync<Cliente>(MethodBase.GetCurrentMethod().Name, data);

                return new ResponseModel<Cliente>(data, count);
            }

            return new ResponseModel<Cliente>(list, list.Count());

        }
        public bool JaExisteCliente(Guid id, string cnpjCpfDi)
        {
            return Buscar(f => f.CnpjCpfDi == cnpjCpfDi && f.Id != id).Result.Any();
        }
        public bool EncontrouCliente(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();    
        }
    }
}
