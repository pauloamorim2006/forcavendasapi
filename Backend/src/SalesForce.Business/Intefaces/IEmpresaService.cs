using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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
        Task<List<Empresa>> ObterTodos();
        Task<Empresa> Buscar();
    }
}
