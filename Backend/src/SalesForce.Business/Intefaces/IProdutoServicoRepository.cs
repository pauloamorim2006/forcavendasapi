using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IProdutoServicoRepository : IRepository<ProdutoServico>
    {
        Task<ResponseModel<ProdutoServico>> RecuperarTodos(PaginationFilter filter);
        Task<int> RecuperarQuantidade();
        bool JaExiste(Guid id, string nome);
        bool Encontrou(Guid id);
    }
}
