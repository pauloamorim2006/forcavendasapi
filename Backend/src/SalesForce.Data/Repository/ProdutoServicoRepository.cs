using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using Microsoft.EntityFrameworkCore;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Data.Repository
{
    public class ProdutoServicoRepository : Repository<ProdutoServico>, IProdutoServicoRepository
    {
        public ProdutoServicoRepository(SalesForceDbContext context) : base(context) {}

        public async Task<ResponseModel<ProdutoServico>> RecuperarTodos(PaginationFilter filter)
        {
            var total =
                await Db.ProdutosServicos.CountAsync();
            var data = await Db.ProdutosServicos
                .Include(x => x.Unidade)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync();
            return new ResponseModel<ProdutoServico>(data, total);
        }
        public async Task<int> RecuperarQuantidade()
        {
            return await Db.ProdutosServicos.AsNoTracking().CountAsync();
        }
        public bool JaExiste(Guid id, string nome)
        {
            return Buscar(f => f.Nome == nome && f.Id != id).Result.Any();
        }
        public bool Encontrou(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();
        }
    }
}
