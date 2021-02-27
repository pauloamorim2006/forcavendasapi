using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IProdutoServicoService
    {
        Task<List<ProdutoServico>> RecuperarTodos();
        Task<int> RecuperarQuantidade();
        Task<IEnumerable<ProdutoServico>> Buscar(Expression<Func<ProdutoServico, bool>> predicate);
        Task<ProdutoServico> ObterPorId(Guid id);
        Task<ProdutoServico> Obter(Guid id);
        Task<List<ProdutoServico>> ObterTodos();
        Task<bool> Adicionar(ProdutoServico produtoServico);
        Task<bool> Atualizar(ProdutoServico produtoServico);
        Task<bool> Remover(Guid id);
    }
}
