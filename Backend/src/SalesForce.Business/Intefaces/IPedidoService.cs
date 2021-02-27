using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IPedidoService
    {
        Task<bool> Adicionar(Pedido pedido);
        Task<bool> Atualizar(Pedido pedido);
        Task<bool> Remover(Guid id);
        Task<bool> Cancelar(Guid id);
        Task<List<double>> RecuperarPorMes();
        Task<IEnumerable<Pedido>> Buscar(Expression<Func<Pedido, bool>> predicate);
        Task<Pedido> ObterPorId(Guid id);
        Task<Pedido> Obter(Guid id);
        Task<List<Pedido>> ObterTodos();
        Task<List<Pedido>> RecuperarTodos(
            DateTime? DataInicial,
            DateTime? DataFinal,
            Guid? ClienteId,
            int? Status);
        Task<Pedido> RecuperarPorId(Guid id);
        Task<int> RecuperarQuantidade();
    }
}
