using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<List<Pedido>> RecuperarTodos(
            DateTime? DataInicial,
            DateTime? DataFinal,
            Guid? ClienteId,
            int? Status);
        Task<Pedido> RecuperarPorId(Guid id);
        Task<int> RecuperarQuantidade();
        bool JaExiste(Guid id, int codigo);
        bool Encontrou(Guid id);
    }
}
