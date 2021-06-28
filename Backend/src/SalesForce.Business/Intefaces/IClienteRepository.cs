using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<ResponseModel<Cliente>> RecuperarTodos(PaginationFilter filter);
        Task<int> RecuperarQuantidade();
        bool JaExisteCliente(Guid id, string cnpjCpfDi);
        bool EncontrouCliente(Guid id);
    }
}
