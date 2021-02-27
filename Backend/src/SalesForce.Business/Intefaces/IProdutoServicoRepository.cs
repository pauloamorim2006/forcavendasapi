using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IProdutoServicoRepository : IRepository<ProdutoServico>
    {
        Task<List<ProdutoServico>> RecuperarTodos();
        Task<int> RecuperarQuantidade();
        bool JaExiste(Guid id, string nome);
        bool Encontrou(Guid id);
    }
}
