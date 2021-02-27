using ERP.Business.Models;
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
        Task<List<Cliente>> ObterTodos();
        Task<bool> Adicionar(Cliente cliente);
        Task<bool> Atualizar(Cliente cliente);
        Task<bool> Remover(Guid id);
        Task<List<Cliente>> RecuperarTodos();
        Task<int> RecuperarQuantidade();
    }
}
