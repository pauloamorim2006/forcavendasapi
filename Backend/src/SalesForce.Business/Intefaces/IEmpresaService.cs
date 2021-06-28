using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IEmpresaService
    {
        Task<bool> Adicionar(Empresa empresa);
        Task<bool> Atualizar(Empresa empresa);
        Task<bool> Remover(Guid id);
        Task<IEnumerable<Empresa>> Buscar(Expression<Func<Empresa, bool>> predicate);
        Task<Empresa> ObterPorId(Guid id);
        Task<Empresa> Obter(Guid id);
        Task<ResponseModel<Empresa>> ObterTodos(PaginationFilter filter);
        Task<Empresa> Buscar();
    }
}
