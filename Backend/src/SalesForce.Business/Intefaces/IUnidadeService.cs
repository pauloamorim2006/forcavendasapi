using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IUnidadeService
    {
        Task<IEnumerable<Unidade>> Buscar(Expression<Func<Unidade, bool>> predicate);
        Task<Unidade> ObterPorId(Guid id);
        Task<Unidade> Obter(Guid id);
        Task<List<Unidade>> ObterTodos();
        Task<List<Unidade>> RecuperarTodos();
        Task<bool> Adicionar(Unidade unidade);
        Task<bool> Atualizar(Unidade unidade);
        Task<bool> Remover(Guid id);
    }
}
