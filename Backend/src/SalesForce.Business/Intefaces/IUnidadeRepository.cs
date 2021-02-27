using ERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IUnidadeRepository : IRepository<Unidade>
    {
        Task<List<Unidade>> RecuperarTodos();
        bool JaExiste(Guid id, string sigla);
        bool Encontrou(Guid id);
    }
}
