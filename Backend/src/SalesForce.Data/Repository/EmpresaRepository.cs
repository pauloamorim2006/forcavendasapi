using System;
using System.Linq;
using System.Threading.Tasks;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using Microsoft.EntityFrameworkCore;
using SalesForce.Data.Cache;

namespace ERP.Data.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(SalesForceDbContext context, ICache cache) : base(context, cache) { }

        public async Task<Empresa> Buscar()
        {
            return await Db.Empresas.AsNoTracking().Include(x => x.Cidade).FirstOrDefaultAsync();
        }
        public bool JaExiste(Guid id, string cnpjCpfDi)
        {
            return Buscar(f => f.CnpjCpfDi == cnpjCpfDi && f.Id != id).Result.Any();
        }
        public bool Encontrou(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();
        }
    }
}
