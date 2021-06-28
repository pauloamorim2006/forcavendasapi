using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate);
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> Obter(Guid id);
        Task<ResponseModel<Cliente>> ObterTodos(PaginationFilter filter);
        Task<bool> Adicionar(Cliente cliente);
        Task<bool> Atualizar(Cliente cliente);
        Task<bool> Remover(Guid id);
        Task<ResponseModel<Cliente>> RecuperarTodos(PaginationFilter filter);
        Task<int> RecuperarQuantidade();
    }
}
