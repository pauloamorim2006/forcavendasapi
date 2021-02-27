using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface ICidadeService
    {
        Task<IEnumerable<Cidade>> Buscar(Expression<Func<Cidade, bool>> predicate);
        Task<Cidade> ObterPorId(Guid id);
        Task<Cidade> Obter(Guid id);
        Task<List<Cidade>> ObterTodos();
        Task<bool> Adicionar(Cidade cidade);
        Task<bool> Atualizar(Cidade cidade);
        Task<bool> Remover(Guid id);
    }
}
