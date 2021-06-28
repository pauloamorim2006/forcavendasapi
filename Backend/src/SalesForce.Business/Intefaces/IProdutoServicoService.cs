using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IProdutoServicoService
    {
        Task<ResponseModel<ProdutoServico>> RecuperarTodos(PaginationFilter filter);
        Task<int> RecuperarQuantidade();
        Task<IEnumerable<ProdutoServico>> Buscar(Expression<Func<ProdutoServico, bool>> predicate);
        Task<ProdutoServico> ObterPorId(Guid id);
        Task<ProdutoServico> Obter(Guid id);
        Task<ResponseModel<ProdutoServico>> ObterTodos(PaginationFilter filter);
        Task<bool> Adicionar(ProdutoServico produtoServico);
        Task<bool> Atualizar(ProdutoServico produtoServico);
        Task<bool> Remover(Guid id);
    }
}
