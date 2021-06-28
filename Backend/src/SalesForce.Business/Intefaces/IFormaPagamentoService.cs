using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IFormaPagamentoService
    {
        Task<IEnumerable<FormaPagamento>> Buscar(Expression<Func<FormaPagamento, bool>> predicate);
        Task<FormaPagamento> ObterPorId(Guid id);
        Task<FormaPagamento> Obter(Guid id);
        Task<ResponseModel<FormaPagamento>> ObterTodos(PaginationFilter filter);
        Task<bool> Adicionar(FormaPagamento formaPagamento);
        Task<bool> Atualizar(FormaPagamento formaPagamento);
        Task<bool> Remover(Guid id);
    }
}
