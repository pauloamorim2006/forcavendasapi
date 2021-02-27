using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<List<Cliente>> RecuperarTodos();
        Task<int> RecuperarQuantidade();
        bool JaExisteCliente(Guid id, string cnpjCpfDi);
        bool EncontrouCliente(Guid id);
    }
}
